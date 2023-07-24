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
console.log("Document height: ", document.documentElement.scrollHeight);
console.log("Window height: ", window.innerHeight);
window.addEventListener("DOMContentLoaded", (event) => {
    let navbar = document.querySelector('.custom-O-navbar');
    let lastScrollTop = 0;
    window.addEventListener("scroll", function () {
        let scrollTop = window.pageYOffset || document.documentElement.scrollTop;
        if (scrollTop > lastScrollTop) {
            navbar.style.opacity = 0;
        } else {
            navbar.style.opacity = 1;
        }
        lastScrollTop = scrollTop;

        // If scroll is at the top of the screen, show navbar
        if (window.scrollY === 0) {
            navbar.style.opacity = 1;
        }
    });
});
//document.addEventListener("DOMContentLoaded", function () {
//    console.log("Hello world");

//    var lastScrollTop = 0;
//    var navbar = document.querySelector('.custom-O-navbar');

//    window.addEventListener('scroll', function () {
//        console.log("Scroll event fired");
//        var scrollTop = window.pageYOffset || document.documentElement.scrollTop;

//        if (navbar !== null) {
//            if (scrollTop > lastScrollTop && scrollTop > navbar.offsetHeight) {
//                // Downscroll, hide navbar
//                navbar.style.opacity = "0";
//            } else {
//                // Upscroll or reach top, show navbar
//                navbar.style.opacity = "1";
//            }
//        }
//        console.log("scrolled");
//        lastScrollTop = scrollTop;
//    });
//});
// Function to handle Sidebar
function handleSidebarToggle() {
    const sidebar = document.querySelector(".Custom-o-sidebar");
    sidebar.classList.toggle("open");
};

function updateCount(value) {
    var len = value.length;
    document.getElementById('charCount').innerText = len + "/750";
};
function TitleupdateCount(value) {
    var len = value.length;
    document.getElementById('TitleCount').innerText = len + "/50";
}; 