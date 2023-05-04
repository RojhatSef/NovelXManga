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
console.log('site.js loaded');
(function () {
    var selectedTags = $('#selectedTags').val().split(',');
    $('input[type=checkbox]').each(function () {
        if (selectedTags.includes($(this).val())) {
            $(this).prop('checked', true);
        }
    });

    $('input[type=checkbox]').on('change', function () {
        var selectedTags = $('input[type=checkbox]:checked').map(function () {
            return $(this).val();
        }).get();
        $('#selectedTags').val(selectedTags.join());
    });
})
console.log('site.js loaded2');
const tagDropdown = document.getElementById('tag-dropdown');
/*const tagOptions = tagDropdown.options;*/

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
console.log('site.js loaded3');
document.addEventListener("DOMContentLoaded", () => {
    const navbar = document.querySelector(".navbar");
    const sidebar = document.querySelector(".sidebar");
    const sidebarToggle = document.querySelector("#sidebar-toggle");

    let scrollPosition = window.pageYOffset;

    window.addEventListener("scroll", () => {
        const currentScrollPosition = window.pageYOffset;

        if (scrollPosition > currentScrollPosition) {
            navbar.classList.remove("hidden");
        } else {
            navbar.classList.add("hidden");
        }

        scrollPosition = currentScrollPosition;
    });

    sidebarToggle.addEventListener("click", () => {
        sidebar.classList.toggle("open");
    });
});

//function openNav() {
//    var container = $(".container-costum");

//    if (container.css("grid-template-areas") === '"SidebarCostum nav nav nav" "SidebarCostum main main main"') {
//        container.css("grid-template-areas", '"nav nav nav nav" "main main main main"');
//        container.css("grid-template-columns", "1fr 1fr 1fr 1fr");
//    } else {
//        container.css("grid-template-areas", '"SidebarCostum nav nav nav" "SidebarCostum main main main"');
//        container.css("grid-template-columns", "1fr 3fr");
//    }
//}