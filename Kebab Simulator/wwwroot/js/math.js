window.initMath = function () {
    const qEl = document.getElementById("math-q");
    const aEl = document.getElementById("math-a");
    const sEl = document.getElementById("math-status");
    const btn = document.getElementById("math-submit");
    const timeLeftEl = document.getElementById("timeLeft");
    const bonusEl = document.getElementById("bonus");
    if (!qEl || !aEl || !sEl || !btn || !timeLeftEl || !bonusEl) return;
    let timeLeft = parseInt(timeLeftEl.textContent) || 30;
    let bonus = parseInt(bonusEl.textContent) || 0;
    const timer = setInterval(() => {
        timeLeft--;
        timeLeftEl.textContent = timeLeft;
        if (timeLeft <= 0) {
            clearInterval(timer);
            sEl.innerHTML = "<span class='text-warning'>Time is up! 🍖</span>";
            aEl.disabled = true; btn.disabled = true;
        }
    }, 1000);
    function newTask() {
        const a = Math.floor(Math.random() * 10) + 1;
        const b = Math.floor(Math.random() * 9) + 1;
        const ops = ['+', '-', '×', '÷', '^'];
        const op = ops[Math.floor(Math.random() * ops.length)];
        let ans, text;
        if (op === '÷') {
            const k = Math.floor(Math.random() * 9) + 1;
            ans = k; text = `${b * k} ÷ ${b} = ?`;
        } else if (op === '^') {
            ans = a * a; text = `${a} ^ 2 = ?`;
        } else if (op === '+') {
            ans = a + b; text = `${a} + ${b} = ?`;
        } else if (op === '-') {
            ans = a - b; text = `${a} - ${b} = ?`;
        } else {
            ans = a * b; text = `${a} × ${b} = ?`;
        }
        qEl.dataset.answer = String(ans);
        qEl.textContent = text;
        aEl.value = ""; aEl.focus();
    }
    function check() {
        if (timeLeft <= 0) return;
        const correct = Number(qEl.dataset.answer);
        const given = Number(aEl.value);
        if (Number.isFinite(given) && given === correct) {
            timeLeft = Math.max(0, timeLeft - 3);
            bonus += 3;
            timeLeftEl.textContent = timeLeft;
            bonusEl.textContent = bonus;
            sEl.innerHTML = "<span class='text-success fw-bold'>Correct! −3s</span>";
            newTask();
            if (timeLeft <= 0) {
                clearInterval(timer);
                sEl.innerHTML = "<span class='text-warning'>Time is up! 🍖</span>";
                aEl.disabled = true; btn.disabled = true;
            }
        } else {
            timeLeft += 5;
            timeLeftEl.textContent = timeLeft;
            sEl.innerHTML = "<span class='text-danger fw-bold'>Wrong! +5s</span>";
            qEl.animate(
                [{ transform: "translateX(0)" }, { transform: "translateX(-6px)" }, { transform: "translateX(6px)" }, { transform: "translateX(0)" }],
                { duration: 180 }
            );
        }
    }
    btn.addEventListener("click", check);
    aEl.addEventListener("keydown", (e) => { if (e.key === "Enter") check(); });
    newTask();
};
