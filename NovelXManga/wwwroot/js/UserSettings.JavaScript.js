document.addEventListener('DOMContentLoaded', function () {
    const toggle = document.querySelector('.toggle-input');

    // Event listener for toggle change
    toggle.addEventListener('change', function () {
        // Synchronize the input's checked state with the form input bound to the Razor model
        document.querySelector('input[name="UserSettings.DarkModeEnabled"]').checked = toggle.checked;
    });
});