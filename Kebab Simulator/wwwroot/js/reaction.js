window.initReaction = function () {
    const rxBox = document.getElementById("rx-box");
    const rxStart = document.getElementById("rx-start");
    const rxStatus = document.getElementById("rx-status");
    const timeLeftEl = document.getElementById("timeLeft");
    const bonusEl = document.getElementById("bonus");
    if (!rxBox || !rxStart || !rxStatus || !timeLeftEl || !bonusEl) return;
    let timeLeft = parseInt(timeLeftEl.textContent) || 30;
    let bonus = parseInt(bonusEl.textContent) || 0;
    let armed = false;
    let isGreen = false;
    let reacted = false;
    let startTs = 0;
    let toId = null;
    function cancelPendingGreen() { if (toId) { clearTimeout(toId); toId = null; } }
    function resetRoundUI() {
        rxBox.style.background = "#3a3a3a";
        rxBox.textContent = "Press Start to begin.";
        rxStatus.textContent = "";
        rxStart.disabled = false;
        armed = false; isGreen = false; reacted = false;
    }
    const timer = setInterval(() => {
        timeLeft = Math.max(0, timeLeft - 1);
        timeLeftEl.textContent = timeLeft;
        if (timeLeft <= 0) {
            clearInterval(timer);
            cancelPendingGreen();
            rxBox.style.background = "#3a3a3a";
            rxBox.textContent = "Time is up! 🍖";
            rxStart.disabled = true;
            armed = false; isGreen = false; reacted = true;
        }
    }, 1000);
    rxStart.addEventListener("click", () => {
        if (timeLeft <= 0) return;
        rxStatus.textContent = "Wait for green...";
        rxBox.style.background = "#3a3a3a";
        rxBox.textContent = "Get ready...";
        rxStart.disabled = true;
        armed = true; isGreen = false; reacted = false;
        cancelPendingGreen();
        toId = setTimeout(() => {
            if (timeLeft <= 0) return;
            rxBox.style.background = "#2e7d32";
            rxBox.textContent = "NOW!";
            isGreen = true;
            startTs = performance.now();
            rxStatus.textContent = "";
            rxStart.disabled = false;
            toId = null;
        }, 1000 + Math.random() * 2000);
    });
    rxBox.addEventListener("click", () => {
        if (!armed || reacted || timeLeft <= 0) return;
        if (!isGreen) {
            reacted = true;
            timeLeft = timeLeft + 3;
            timeLeftEl.textContent = timeLeft;
            cancelPendingGreen();
            rxBox.style.background = "#b71c1c";
            rxBox.textContent = "Too early! +3s. Restarting...";
            rxStatus.textContent = "Wait for green next time!";
            setTimeout(() => { resetRoundUI(); }, 900);
            return;
        }
        reacted = true;
        const rt = performance.now() - startTs;
        let gain = 0;
        if (rt < 250) gain = 3;
        else if (rt < 500) gain = 2;
        else if (rt < 800) gain = 1;
        if (gain > 0) {
            timeLeft = Math.max(0, timeLeft - gain);
            bonus += gain;
            timeLeftEl.textContent = timeLeft;
            bonusEl.textContent = bonus;
            rxStatus.textContent = `${Math.round(rt)} ms → −${gain}s`;
            rxBox.style.background = "#3a3a3a";
            rxBox.textContent = "Nice reflex!";
        } else {
            rxStatus.textContent = `${Math.round(rt)} ms — Too slow 😅`;
            rxBox.style.background = "#3a3a3a";
            rxBox.textContent = "Try again!";
        }
        armed = false; isGreen = false; rxStart.disabled = false;
    });
};
