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
            navbar.style.visibility = "hidden";
        } else {
            navbar.style.opacity = 1;
            navbar.style.visibility = "visible";
            navbar.style.backgroundColor = '#191a1c'; // Add the background color when scrolling up
            navbar.style.borderBottom = '1px solid #ff6740'; // Add the border bottom when scrolling up
        }
        lastScrollTop = scrollTop;

        // If scroll is at the top of the screen, show navbar and change styles
        if (window.scrollY === 0) {
            navbar.style.opacity = 1;
            navbar.style.visibility = "visible";
            navbar.style.backgroundColor = 'transparent'; // Make the background transparent at the top of the page
            navbar.style.borderBottom = 'none'; // Remove the border bottom at the top of the page
        }
    });
});
$(document).ready(function () {
    $('.bar span').hide();

    var maxReviews = 0;
    for (var score in scoreDistribution) {
        if (scoreDistribution[score] > maxReviews) {
            maxReviews = scoreDistribution[score];
        }
    }

    for (var score in scoreDistribution) {
        var percentage = (scoreDistribution[score] / maxReviews) * 100;
        var barId = '#bar-';
        switch (parseInt(score)) {
            case 5: barId += 'five'; break;
            case 4: barId += 'four'; break;
            case 3: barId += 'three'; break;
            case 2: barId += 'two'; break;
            case 1: barId += 'one'; break;
        }

        $(barId).animate({
            width: percentage + '%'
        }, 1000);
    }

    setTimeout(function () {
        $('.bar span').fadeIn('slow');
    }, 1000);
});

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

(function () {
    let containers = document.querySelectorAll(".rating-container");
    containers.forEach(container => {
        let stars = container.querySelectorAll("input");
        let category = container.getAttribute("data-category");
        let showValue = document.querySelector("#rating-" + category);
        stars.forEach(star => {
            star.addEventListener("click", function () {
                let value = this.value;
                showValue.textContent = value + " Out of 5";

                // If you need to send this data to the backend immediately, you can do so here
                // sendToBackend(category, value);
            });
        });
    });
})();