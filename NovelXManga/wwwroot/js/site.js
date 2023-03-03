function handleScreenSizeChange() {
    if (window.matchMedia("(max-width: 767px)").matches) {
        document.documentElement.classList.remove("large-screen");
        document.documentElement.classList.add("small-screen");
    } else {
        document.documentElement.classList.remove("small-screen");
        document.documentElement.classList.add("large-screen");
    }
}

function handleSidebarClick(event) {
    const sidebar = document.querySelector('.sidebar');
    const sidebarToggle = document.querySelector('#sidebar-toggle');

    sidebar.classList.toggle('show');
    if (sidebar.classList.contains('show')) {
        sidebar.style.transform = 'translateX(0)';
    } else {
        sidebar.style.transform = 'translateX(-100%)';
        sidebar.classList.remove('show');
    }
}

document.addEventListener('DOMContentLoaded', () => {
    handleScreenSizeChange();
    window.addEventListener('resize', handleScreenSizeChange);
});

const sidebarToggle = document.querySelector('#sidebar-toggle');

sidebarToggle.addEventListener('click', handleSidebarClick);

// Select all links with the "sidebar-link" class
var links = document.querySelectorAll('.sidebar-link');

const sidebarItems = document.querySelectorAll('#SidebarColors');