document.addEventListener('DOMContentLoaded', function () {
    var themeContainer = document.querySelector('.theme-container');
    var isDarkModeEnabled = themeContainer.getAttribute('data-dark-mode') === 'True';

    function applyTheme(darkMode) {
        var body = document.body;
        if (darkMode) {
            body.classList.remove('light-theme');
            body.classList.add('dark-theme');
        } else {
            body.classList.remove('dark-theme');
            body.classList.add('light-theme');
        }
    }

    applyTheme(isDarkModeEnabled);
});