document.addEventListener('DOMContentLoaded', function () {
    const toggle = document.querySelector('.toggle-input');

    // Event listener for toggle change
    toggle.addEventListener('change', function () {
        // Synchronize the input's checked state with the form input bound to the Razor model
        document.querySelector('input[name="UserSettings.DarkModeEnabled"]').checked = toggle.checked;
    });
});

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