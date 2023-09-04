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

//function searchManga(searchTerm) {
//    var dropdown = document.getElementById('mangaResultsDropdown');

//    if (searchTerm.length >= 2) {
//        fetch(`/Index?handler=SearchManga&searchTerm=${searchTerm}`)
//            .then(response => response.json())
//            .then(data => {
//                dropdown.innerHTML = data.map(manga => `
//                    <div class="dropdown-item">
//                        <a href="/Manga/CurrentManga?id=${manga.mangaID}">
//                    <img src="/Images/GeneratedMangaImage/${manga.photoPath}" alt="${manga.mangaName}">
//                            <span data-mangaName="${manga.mangaName}">
//                                ${manga.mangaName}
//                            </span>
//                        </a>
//                    </div>
//                `).join('');
//                dropdown.style.display = 'block';
//            });
//    } else {
//        dropdown.innerHTML = "";
//        dropdown.style.display = 'none';
//    }
////}

///*Old searchmanga code*/
function searchManga(searchTerm) {
    var dropdown = document.getElementById('mangaResultsDropdown');

    if (searchTerm.length >= 2) {
        fetch(`/Index?handler=SearchManga&searchTerm=${searchTerm}`)
            .then(response => response.json())
            .then(data => {
                dropdown.innerHTML = data.map(manga => `

                    <div class="dropdown-item">
                        <a href="/Manga/CurrentManga?id=${manga.mangaID}">
                    <img src="/Images/GeneratedMangaImage/${manga.photoPath}" alt="${manga.mangaName}">
                            <span data-mangaName="${manga.mangaName}">
                                ${manga.mangaName}
                            </span>
                        </a>

                    </div>

                `).join('');
                dropdown.style.display = 'block';
            });
    } else {
        dropdown.innerHTML = "";
        dropdown.style.display = 'none';
    }
}

////New Searchmanga code
//function searchManga(searchTerm) {
//    var dropdown = document.getElementById('mangaResultsDropdown');
//    if (searchTerm.length >= 2) {
//        fetch(`/Index?handler=SearchManga&searchTerm=${searchTerm}`)
//            .then(response => response.json())
//            .then(data => {
//                const books = data.filter(item => item.Type === "Manga");
//                const authors = data.filter(item => item.Type === "Author");
//                const artists = data.filter(item => item.Type === "Artist");
//                const voiceActors = data.filter(item => item.Type === "VoiceActor");

//                let htmlString = '';

//                if (books.length > 0) {
//                    htmlString += `<h1>Books</h1>`;
//                    books.forEach(book => {
//                        htmlString += `
//                            <div class="dropdown-item Manga">
//                                <a href="/Manga/Details?id=${book.MangaID}">
//                                    <img src="/Images/GeneratedMangaImage/${book.PhotoPath}" alt="${book.MangaName}">
//                                    <span>${book.MangaName}</span>
//                                </a>
//                            </div>
//                        `;
//                    });
//                }

//                if (authors.length > 0) {
//                    htmlString += `<h1>Authors</h1>`;
//                    authors.forEach(author => {
//                        htmlString += `
//                            <div class="dropdown-item Author">
//                                <a href="/Author/Details?id=${author.AuthorID}">
//                                    <span>${author.FirstName} ${author.LastName}</span>
//                                </a>
//                            </div>
//                        `;
//                    });
//                }

//                if (artists.length > 0) {
//                    htmlString += `<h1>Artists</h1>`;
//                    artists.forEach(artist => {
//                        htmlString += `
//                            <div class="dropdown-item Artist">
//                                <a href="/Artist/Details?id=${artist.ArtistId}">
//                                    <span>${artist.FirstName} ${artist.LastName}</span>
//                                </a>
//                            </div>
//                        `;
//                    });
//                }

//                if (voiceActors.length > 0) {
//                    htmlString += `<h1>Voice Actors</h1>`;
//                    voiceActors.forEach(voiceActor => {
//                        htmlString += `
//                            <div class="dropdown-item VoiceActor">
//                                <a href="/VoiceActor/Details?id=${voiceActor.VoiceActorId}">
//                                    <span>${voiceActor.FirstName} ${voiceActor.LastName}</span>
//                                </a>
//                            </div>
//                        `;
//                    });
//                }

//                dropdown.innerHTML = htmlString;
//                dropdown.style.display = 'block';
//            });
//    } else {
//        dropdown.innerHTML = "";
//        dropdown.style.display = 'none';
//    }
//}