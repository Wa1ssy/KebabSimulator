(function () {
    const THEME_KEY = "ksim_theme";

    function applyTheme() {
        const theme = localStorage.getItem(THEME_KEY) || "light";
        document.documentElement.setAttribute("data-theme", theme);
    }

    function toggleTheme() {
        const current = document.documentElement.getAttribute("data-theme") || "light";
        const next = current === "dark" ? "light" : "dark";
        localStorage.setItem(THEME_KEY, next);
        applyTheme();
    }

    document.addEventListener("DOMContentLoaded", () => {
        applyTheme();
        const btn = document.getElementById("theme-toggle");
        if (btn) btn.addEventListener("click", toggleTheme);
    });
})();
