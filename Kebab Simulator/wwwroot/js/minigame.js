window.initClicker = function () {
    const grillBtn = document.getElementById("grillButton");
    const timeLeftEl = document.getElementById("timeLeft");
    const bonusEl = document.getElementById("bonus");
    const zone = document.getElementById("grill-zone") || document.getElementById("minigame-area");
    if (!grillBtn || !timeLeftEl || !bonusEl || !zone) return;
    if (getComputedStyle(zone).position === "static") zone.style.position = "relative";
    grillBtn.style.position = "absolute";
    let timeLeft = parseInt(timeLeftEl.textContent, 10);
    if (Number.isNaN(timeLeft)) timeLeft = 30;
    let bonus = parseInt(bonusEl.textContent, 10);
    if (Number.isNaN(bonus)) bonus = 0;
    const timer = setInterval(() => {
        timeLeft = Math.max(0, timeLeft - 1);
        timeLeftEl.textContent = timeLeft;
        if (timeLeft <= 0) {
            clearInterval(timer);
            grillBtn.disabled = true;
            grillBtn.setAttribute("aria-disabled", "true");
        }
    }, 1000);
    function moveInside() {
        const maxX = Math.max(0, zone.clientWidth - grillBtn.offsetWidth);
        const maxY = Math.max(0, zone.clientHeight - grillBtn.offsetHeight);
        const x = Math.floor(Math.random() * (maxX + 1));
        const y = Math.floor(Math.random() * (maxY + 1));
        grillBtn.style.left = x + "px";
        grillBtn.style.top = y + "px";
    }
    moveInside();
    grillBtn.addEventListener("click", (e) => {
        e.stopPropagation();
        if (timeLeft <= 1) return;
        timeLeft = Math.max(0, timeLeft - 1);
        bonus += 1;
        timeLeftEl.textContent = timeLeft;
        bonusEl.textContent = bonus;
        moveInside();
        grillBtn.animate(
            [{ transform: "scale(1)" }, { transform: "scale(1.08)" }, { transform: "scale(1)" }],
            { duration: 150 }
        );
    });
    zone.addEventListener("click", (e) => {
        if (timeLeft <= 0) return;
        if (e.target === grillBtn) return;
        timeLeft += 5;
        timeLeftEl.textContent = timeLeft;
        const prev = zone.style.boxShadow;
        zone.style.boxShadow = "0 0 20px rgba(183,28,28,0.9)";
        setTimeout(() => { zone.style.boxShadow = prev; }, 180);
    });
    window.addEventListener("resize", moveInside);
};
