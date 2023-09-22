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
// sidebar click on the dropdown menu

//new Code This is the search for Books/artist/Author/voice actors which is on the layout page SEARCH. Rest of the backend is in the Index page.
function searchManga(searchTerm) {
    var dropdown = document.getElementById('mangaResultsDropdown');

    if (searchTerm.length >= 2) {
        fetch(`/Index?handler=SearchManga&searchTerm=${sanitizeHTML(searchTerm)}`)
            .then(response => response.json())
            .then(data => {
                console.log(data);  // Debugging line
                let allResults = [];
                // Manga Results
                if (data.Manga) {
                    allResults.push('<h1>Books:</h1>');
                    let mangaResults = data.Manga.map(manga => `
                <div class="dropdown-item">
                    <a href="/Manga/CurrentManga?id=${sanitizeHTML(manga.id)}">
                        <img src="/Images/GeneratedMangaImage/${sanitizeHTML(manga.photoPath)}" alt="${sanitizeHTML(manga.name)}">
                        <span data-mangaName="${sanitizeHTML(manga.name)}">
                            ${sanitizeHTML(manga.name)}
                        </span>
                    </a>
                </div>
            `);
                    allResults.push(mangaResults.join(''));
                }
                // Author Results
                if (data.Author) {
                    allResults.push('<h1>Authors:</h1>');
                    let authorResults = data.Author.map(author => `
                        <div class="dropdown-item">
                            <span data-authorName="${sanitizeHTML(author.name)}">
                                ${sanitizeHTML(author.name)}
                            </span>
                        </div>
                    `);
                    allResults.push(authorResults.join(''));
                }

                // Artist Results
                if (data.Artist) {
                    allResults.push('<h1>Artists:</h1>');
                    let artistResults = data.Artist.map(artist => `
                        <div class="dropdown-item">
                            <span data-artistName="${sanitizeHTML(artist.name)}">
                                ${sanitizeHTML(artist.name)}
                            </span>
                        </div>
                    `);
                    allResults.push(artistResults.join(''));
                }

                // Voice Actor Results
                if (data.VoiceActor) {
                    allResults.push('<h1>Voice Actors:</h1>');
                    let voiceActorResults = data.VoiceActor.map(voiceActor => `
                        <div class="dropdown-item">
                            <span data-voiceActorName="${sanitizeHTML(voiceActor.name)}">
                                ${sanitizeHTML(voiceActor.name)}
                            </span>
                        </div>
                    `);
                    allResults.push(voiceActorResults.join(''));
                }

                dropdown.innerHTML = allResults.join('');
                dropdown.style.display = 'block';
            });
    } else {
        dropdown.innerHTML = "";
        dropdown.style.display = 'none';
    }
} function sanitizeHTML(text) {
    var element = document.createElement('div');
    element.textContent = text;
    return element.innerHTML;
}

document.getElementById('dropdownToggle').addEventListener('click', function () {
    const dropdown = document.getElementById('userDropdown');
    if (dropdown.classList.contains('show')) {
        dropdown.classList.remove('show');
    } else {
        dropdown.classList.add('show');
    }
});

// To hide dropdown when clicking outside of it
document.addEventListener('click', function (event) {
    const dropdown = document.getElementById('userDropdown');
    const toggle = document.getElementById('dropdownToggle');

    if (!dropdown.contains(event.target) && !toggle.contains(event.target)) {
        dropdown.classList.remove('show');
    }
});