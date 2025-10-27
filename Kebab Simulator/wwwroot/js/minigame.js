// wwwroot/js/minigame.js

document.addEventListener("DOMContentLoaded", function () {
    const grillBtn = document.getElementById("grillButton");
    const timeLeftEl = document.getElementById("timeLeft");
    const bonusEl = document.getElementById("bonus");
    const container = document.getElementById("minigame-area");

    let timeLeft = parseInt(timeLeftEl.textContent) || 30;
    let bonus = 0;

    const timer = setInterval(() => {
        timeLeft--;
        timeLeftEl.textContent = timeLeft;

        if (timeLeft <= 0) {
            clearInterval(timer);
            alert("Kebab valmis!");
            grillBtn.disabled = true;
        }
    }, 1000);

    grillBtn.addEventListener("click", () => {
        if (timeLeft <= 1) return;

        timeLeft--;
        bonus++;
        timeLeftEl.textContent = timeLeft;
        bonusEl.textContent = bonus;

        const areaWidth = container.offsetWidth - grillBtn.offsetWidth;
        const areaHeight = container.offsetHeight - grillBtn.offsetHeight;

        const randomX = Math.floor(Math.random() * areaWidth);
        const randomY = Math.floor(Math.random() * areaHeight);

        grillBtn.style.left = randomX + "px";
        grillBtn.style.top = randomY + "px";
    });
});
