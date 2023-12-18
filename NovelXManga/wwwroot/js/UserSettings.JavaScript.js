﻿document.addEventListener('DOMContentLoaded', function () {
    var themeContainer = document.querySelector('.theme-container');
    var isDarkModeEnabled = themeContainer.getAttribute('data-dark-mode') === 'True';

    function applyTheme(darkMode) {
        var root = document.documentElement;
        if (darkMode) {
            root.style.setProperty('--background-color', '#191a1c');
            root.style.setProperty('--text-color', 'white');
        } else {
            root.style.setProperty('--background-color', 'white');
            root.style.setProperty('--text-color', 'black');
        }
    }

    applyTheme(isDarkModeEnabled);
});