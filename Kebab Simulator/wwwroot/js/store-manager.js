document.addEventListener("DOMContentLoaded", () => {
    const selectScreen = document.getElementById("select-screen");
    const areaWrapper = document.getElementById("minigame-area-wrapper");
    const timerDisplay = document.getElementById("timer");
    const sellBtn = document.getElementById("sellBtn");
    const cookTime = parseInt(areaWrapper.dataset.cooktime || "30", 10);

    const templates = {
        clicker: `
      <div id="minigame-area" class="shadow-lg p-3 rounded"
           style="width:720px; margin:40px auto; padding-bottom:80px; min-height:480px;
                  background: radial-gradient(circle at center,#2b2b2b,#1a1a1a);
                  border:3px solid #ff9800; color:#ffe8a1;">
        <h5 class="text-warning">🔥 Clicker</h5>
        <p>Time left: <span id="timeLeft">${cookTime}</span>s | Bonus: <span id="bonus">0</span>s</p>
        <div id="grill-zone" style="position:relative;height:340px;background:rgba(255,255,255,0.06);border-radius:10px;overflow:hidden;">
          <button id="grillButton" class="btn btn-warning"
                  style="position:absolute;left:250px;top:120px;padding:18px 36px;font-weight:bold;">
            Grill!
          </button>
        </div>
      </div>
    `,
        reaction: `
      <div id="minigame-area" class="shadow-lg p-3 rounded"
           style="width:720px; margin:40px auto; padding-bottom:80px; min-height:480px;
                  background: radial-gradient(circle at center,#2b2b2b,#1a1a1a);
                  border:3px solid #ff9800; color:#ffe8a1;">
        <h5 class="text-info">⚡ Reaction</h5>
        <p>Time left: <span id="timeLeft">${cookTime}</span>s | Bonus: <span id="bonus">0</span>s</p>
        <p>Click only when the box turns green!</p>
        <div id="rx-box" style="height:260px;border-radius:10px;background:#3a3a3a;display:flex;align-items:center;justify-content:center;font-weight:700;color:#fff;">
          Press "Start" to begin.
        </div>
        <div class="mt-2">
          <button id="rx-start" class="btn btn-outline-info">Start</button>
          <span id="rx-status" class="ms-2 text-info"></span>
        </div>
      </div>
    `,
        math: `
      <div id="minigame-area" class="shadow-lg p-3 rounded"
           style="width:720px; margin:40px auto; padding-bottom:80px; min-height:480px;
                  background: radial-gradient(circle at center,#2b2b2b,#1a1a1a);
                  border:3px solid #ff9800; color:#ffe8a1;">
        <h5 class="text-success">🧮 Math</h5>
        <p>Solve quickly: each correct answer reduces time by 3s.</p>
        <p>Time left: <span id="timeLeft">${cookTime}</span>s | Bonus: <span id="bonus">0</span>s</p>
        <div class="d-flex justify-content-center align-items-center gap-2 mb-2">
          <span id="math-q" class="fs-4 fw-bold">3 + 4 = ?</span>
          <input id="math-a" type="number" class="form-control" style="max-width:160px;">
          <button id="math-submit" class="btn btn-success">Submit</button>
        </div>
        <div id="math-status" class="text-warning"></div>
      </div>
    `
    };

    const observer = new MutationObserver(() => {
        const timeEl = document.getElementById("timeLeft");
        if (!timeEl) return;
        const v = parseInt(timeEl.textContent || "0", 10);
        if (v <= 0) {
            timerDisplay.textContent = "✅ Kebab is ready!";
            sellBtn.disabled = false;
            observer.disconnect();
        }
    });

    function startGame(kind) {
        selectScreen.classList.add("d-none");
        areaWrapper.classList.remove("d-none");
        areaWrapper.innerHTML = templates[kind];
        const timeEl = areaWrapper.querySelector("#timeLeft");
        if (timeEl) observer.observe(timeEl, { childList: true, characterData: true, subtree: true });
        if (kind === "clicker" && typeof window.initClicker === "function") window.initClicker();
        if (kind === "reaction" && typeof window.initReaction === "function") window.initReaction();
        if (kind === "math" && typeof window.initMath === "function") window.initMath();
    }

    document.querySelectorAll('#select-screen [data-game]').forEach(btn => {
        btn.addEventListener('click', () => startGame(btn.dataset.game));
    });
});
