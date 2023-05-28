//function handleScreenSizeChange() {
//    if (window.matchMedia("(max-width: 767px)").matches) {
//        document.documentElement.classList.remove("large-screen");
//        document.documentElement.classList.add("small-screen");
//    } else {
//        document.documentElement.classList.remove("small-screen");
//        document.documentElement.classList.add("large-screen");
//    }
//}

//function handleSidebarClick(event) {
//    const sidebar = document.querySelector('.sidebar');
//    const sidebarToggle = document.querySelector('#sidebar-toggle');

//    sidebar.classList.toggle('show');
//    if (sidebar.classList.contains('show')) {
//        sidebar.style.transform = 'translateX(0)';
//    } else {
//        sidebar.style.transform = 'translateX(-100%)';
//        sidebar.classList.remove('show');
//    }
//}

//document.addEventListener('DOMContentLoaded', () => {
//    handleScreenSizeChange();
//    window.addEventListener('resize', handleScreenSizeChange);
//});

//const sidebarToggle = document.querySelector('#sidebar-toggle');

//sidebarToggle.addEventListener('click', handleSidebarClick);

//// Select all links with the "sidebar-link" class
//var links = document.querySelectorAll('.sidebar-link');

//const sidebarItems = document.querySelectorAll('#SidebarColors');

// All above was previous CSS

// For changing tags

console.log('site.js is loading');
(function () {
    var selectedTags = $('#selectedTags').val().split(',');
    $('input[type=checkbox]').each(function () {
        if (selectedTags.includes($(this).val())) {
            $(this).prop('checked', true);
        }
    });
    console.log('site.js is function SelectTag loading');
    $('input[type=checkbox]').on('change', function () {
        var selectedTags = $('input[type=checkbox]:checked').map(function () {
            return $(this).val();
        }).get();
        $('#selectedTags').val(selectedTags.join());
    });
})

const tagDropdown = document.getElementById('tag-dropdown');

window.onload = function () {
    console.log("Scroll event fired onload");

    handleNavbar();
}
// Function to handle Navbar
function handleNavbar() {
    console.log(document); // logs the entire document
    const customOnavbar = document.querySelector(".custom-O-navbar");
    console.log(navbar);
    console.log("Scroll event fired");
    if (!customOnavbar) {
        console.log('Navbar not found!');
        return;
    }

    let scrollPosition = window.pageYOffset;

    window.addEventListener("scroll", () => {
        const currentScrollPosition = window.pageYOffset;

        if (scrollPosition > currentScrollPosition) {
            console.log("Scrolling up...");
            customOnavbar.classList.remove("hidden");
        } else {
            console.log("Scrolling down...");
            customOnavbar.classList.add("hidden");
        }

        console.log('ustomOnavbar class list: ', customOnavbar.classList);

        if (currentScrollPosition === 0) {
            console.log("At top of page...");
            customOnavbar.classList.add("top-scroll");
        } else {
            customOnavbar.classList.remove("top-scroll");
        }

        scrollPosition = currentScrollPosition;
    });
}

// Function to handle Sidebar
function handleSidebarToggle() {
    const sidebar = document.querySelector(".Custom-o-sidebar");
    sidebar.classList.toggle("open");
}

//function openNav() {
//    var container = $(".container-costum");

//    if (container.css("grid-template-areas") === '"SidebarCostum nav nav nav" "SidebarCostum main main main"') {
//        container.css("grid-template-areas", '"nav nav nav nav" "main main main main"');
//        container.css("grid-template-columns", "1fr 1fr 1fr 1fr");
//    } else {
//        container.css("grid-template-areas", '"SidebarCostum nav nav nav" "SidebarCostum main main main"');
//        container.css("grid-template-columns", "1fr 3fr");
//    }
//}/*const tagOptions = tagDropdown.options;*/

//tagDropdown.addEventListener('change', () => {
//    for (let i = 0; i < tagOptions.length; i++) {
//        const tagOption = tagOptions[i];

//        if (tagOption.selected) {
//            if (tagOption.getAttribute('data-selected') !== 'true') {
//                tagOption.setAttribute('data-selected', true);

//                const checkmark = document.createElement('span');
//                checkmark.innerText = '✔';
//                checkmark.style.marginLeft = '5px';
//                checkmark.style.color = 'green';

//                tagOption.appendChild(checkmark);
//            }
//        } else {
//            if (tagOption.getAttribute('data-selected') === 'true') {
//                tagOption.setAttribute('data-selected', false);

//                const checkmark = tagOption.querySelector('span');
//                checkmark.remove();
//            }
//        }
//    }
//});