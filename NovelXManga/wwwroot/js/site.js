//(function () {
//    var selectedTags = $('#selectedTags').val().split(',');
//    $('input[type=checkbox]').each(function () {
//        if (selectedTags.includes($(this).val())) {
//            $(this).prop('checked', true);
//        }
//    });

//    $('input[type=checkbox]').on('change', function () {
//        var selectedTags = $('input[type=checkbox]:checked').map(function () {
//            return $(this).val();
//        }).get();
//        $('#selectedTags').val(selectedTags.join());
//    });
//})

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
document.addEventListener('click', function (event) {
    var dropdown = document.getElementById('mangaResultsDropdown');
    var searchbar = document.getElementById('searchbar');

    var isClickInsideDropdown = dropdown.contains(event.target);
    var isClickInsideSearchbar = searchbar.contains(event.target);

    if (!isClickInsideDropdown && !isClickInsideSearchbar) {
        dropdown.style.display = 'none';
    }
});
document.getElementById('searchbar').addEventListener('focus', function () {
    var dropdown = document.getElementById('mangaResultsDropdown');
    if (dropdown.innerHTML.trim() !== "") {
        dropdown.style.display = 'block';
    }
});

// fetches data such as books/author/voice/aritst to the searchbar
function searchManga(searchTerm) {
    if (searchTerm.length >= 2) {
        fetch(`/Index?handler=SearchManga&searchTerm=${sanitizeHTML(searchTerm)}`)
            .then(response => response.json())
            .then(data => processSearchResults(data));
    } else {
        hideDropdown();
    }
}

function processSearchResults(data) {
    let allResults = [];

    function createResultItem(type, item) {
        let basePath = `/${type}/Current${type}?id=${sanitizeHTML(item.id)}`;
        let imagePath = item.photoPath ?
            `/Images/Generated${type}Image/${sanitizeHTML(item.photoPath)}` :
            `/Images/${type}Image/NoPhoto.png`;

        return `<a href="${basePath}" class="dropdown-item">
                    <img src="${imagePath}" alt="${sanitizeHTML(item.name)}">
                    <span>${sanitizeHTML(item.name)}</span>
                </a>`;
    }

    ['Manga', 'Author', 'Artist', 'VoiceActor'].forEach(category => {
        if (data[category]) {
            allResults.push(`<h1>${category === 'VoiceActor' ? 'Voice Actors' : category + 's'}:</h1>`);
            let categoryResults = data[category].map(item => createResultItem(category, item));
            allResults.push(categoryResults.join(''));
        }
    });

    showDropdown(allResults.join(''));
}

function showDropdown(content) {
    var dropdown = document.getElementById('mangaResultsDropdown');
    dropdown.innerHTML = content;
    dropdown.style.display = 'block';
}

function hideDropdown() {
    var dropdown = document.getElementById('mangaResultsDropdown');
    dropdown.innerHTML = "";
    dropdown.style.display = 'none';
}

document.addEventListener('DOMContentLoaded', function () {
    var searchBar = document.getElementById('searchBar');
    if (searchBar) {
        searchBar.addEventListener('focus', function () {
            let searchTerm = this.value;
            if (searchTerm.length >= 2) {
                searchManga(searchTerm); // This will fetch and show the dropdown if the searchTerm is valid
            }
        });
    } else {
        console.log('searchBar element not found');
    }

    // ... rest of your dropdown related code
});

////old
//function searchManga(searchTerm) {
//    var dropdown = document.getElementById('mangaResultsDropdown');

//    if (searchTerm.length >= 2) {
//        fetch(`/Index?handler=SearchManga&searchTerm=${sanitizeHTML(searchTerm)}`)
//            .then(response => response.json())
//            .then(data => {
//                // Debugging line
//                let allResults = [];
//                // Manga Results
//                if (data.Manga) {
//                    allResults.push('<h1>Books:</h1>');
//                    let mangaResults = data.Manga.map(manga => `
//                <div class="dropdown-item">
//                    <a href="/Manga/CurrentManga?id=${sanitizeHTML(manga.id)}">
//                        <img src="/Images/GeneratedMangaImage/${sanitizeHTML(manga.photoPath)}" alt="${sanitizeHTML(manga.name)}">
//                        <span data-mangaName="${sanitizeHTML(manga.name)}">
//                            ${sanitizeHTML(manga.name)}
//                        </span>
//                    </a>
//                </div>
//            `);
//                    allResults.push(mangaResults.join(''));
//                }
//                // Author Results
//                if (data.Author) {
//                    allResults.push('<h1>Authors:</h1>');
//                    let authorResults = data.Author.map(author => `
//                        <div class="dropdown-item">
//                            <span data-authorName="${sanitizeHTML(author.name)}">
//                                ${sanitizeHTML(author.name)}
//                            </span>
//                        </div>
//                    `);
//                    allResults.push(authorResults.join(''));
//                }

//                // Artist Results
//                if (data.Artist) {
//                    allResults.push('<h1>Artists:</h1>');
//                    let artistResults = data.Artist.map(artist => `
//                        <div class="dropdown-item">
//                            <span data-artistName="${sanitizeHTML(artist.name)}">
//                                ${sanitizeHTML(artist.name)}
//                            </span>
//                        </div>
//                    `);
//                    allResults.push(artistResults.join(''));
//                }

//                // Voice Actor Results
//                if (data.VoiceActor) {
//                    allResults.push('<h1>Voice Actors:</h1>');
//                    let voiceActorResults = data.VoiceActor.map(voiceActor => `
//                        <div class="dropdown-item">
//                            <span data-voiceActorName="${sanitizeHTML(voiceActor.name)}">
//                                ${sanitizeHTML(voiceActor.name)}
//                            </span>
//                        </div>
//                    `);
//                    allResults.push(voiceActorResults.join(''));
//                }

//                dropdown.innerHTML = allResults.join('');
//                dropdown.style.display = 'block';
//            });
//    } else {
//        dropdown.innerHTML = "";
//        dropdown.style.display = 'none';
//    }
//}

// sanitize html, trying to remove injections.
function sanitizeHTML(text) {
    var element = document.createElement('div');
    element.textContent = text;
    return element.innerHTML;
}

document.addEventListener('click', function (event) {
    var dropdownMenu = document.getElementById('userDropdown');
    var isClickInside = document.getElementById('dropdownToggle').contains(event.target);

    if (!isClickInside && dropdownMenu.classList.contains('show')) {
        dropdownMenu.classList.remove('show');
    }
});

document.getElementById('dropdownToggle').addEventListener('click', function (event) {
    var dropdownMenu = document.getElementById('userDropdown');
    dropdownMenu.classList.toggle('show');
    event.stopPropagation(); // Prevent click event from reaching document
});

// Event handler, removes error from console log. Don't remove Comment out in developer phase or bug fixing'
//window.addEventListener('error', function (event) {
//    // Log the error to  internal system
//    console.error('Logged Error: ', event.error);

//    // Prevent the browser's console from showing the error
//    event.preventDefault();

//    // Show a generic message to the user
//    /* alert('An error occurred. Please try again later.');*/
//});