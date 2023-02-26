// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Get the sidebar element
const sidebar = document.querySelector('.sidebar');

// Add an event listener to show the menu labels on hover
sidebar.addEventListener('mouseover', () => {
    sidebar.classList.add('hover');
});

// Add an event listener to hide the menu labels when the mouse leaves the sidebar
sidebar.addEventListener('mouseout', () => {
    sidebar.classList.remove('hover');
});