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

/*const tagDropdown = document.getElementById('tag-dropdown');*/

window.onload = function () {
    console.log("Scroll event fired onload");

    handleNavbar();
}
// Function to handle Navbar
function handleNavbar() {
    console.log(document); // logs the entire document
    const customOnavbar = document.querySelector(".custom-O-navbar");

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
};
