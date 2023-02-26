//const sidebar = document.querySelector('.sidebar');

//// Add an event listener to show the menu labels on hover
//sidebar.addEventListener('mouseover', () => {
//    sidebar.classList.add('hover');
//});

//// Add an event listener to hide the menu labels when the mouse leaves the sidebar
//sidebar.addEventListener('mouseout', () => {
//    sidebar.classList.remove('hover');
//});

//// Get all the nav links in the sidebar
//var navLinks = document.querySelectorAll('.sidebar-nav .nav-link');

//// Loop through each nav link
//for (var i = 0; i < navLinks.length; i++) {
//    // Add an event listener for the click event
//    navLinks[i].addEventListener('click', function (event) {
//        // Prevent the default link behavior
//        event.preventDefault();

//        // Get the submenu for this nav link
//        var submenu = this.nextElementSibling;

//        // Get the parent of the submenu
//        var parent = submenu.parentElement;

//        // If the parent does not have the show class, add it
//        if (!parent.classList.contains('show')) {
//            parent.classList.add('show');
//        }
//    });

//    // Add an event listener for the mouseleave event
//    navLinks[i].addEventListener('mouseleave', function (event) {
//        // Get the submenu for this nav link
//        var submenu = this.nextElementSibling;

//        // If the submenu is currently open
//        if (submenu.classList.contains('show')) {
//            // Add a one-time event listener for the mouseenter event
//            submenu.addEventListener('mouseenter', function (event) {
//                // Remove the show class from the parent of the submenu
//                this.parentElement.classList.remove('show');
//            }, { once: true });
//        }
//    });
//}