//UserProfile.js
//Reading
function loadMoreReading() {
    let skip = 4; // Starting point for additional items
    const take = 100; // Number of additional items to take
    const loadMoreButton = document.getElementById("UPI-loadMoreReading");
    const customHr = document.querySelector('.UPI-custom-hr');
    const bookList = document.querySelector('.UPI-readingList');
    const userId = loadMoreButton.getAttribute('data-user-id');

    loadMoreButton.addEventListener("click", function () {
        adjustLayoutForExpandedList(loadMoreButton.closest('.UPI-listContainer'));
        fetch(`/UserInteractions/UserProfile?handler=LoadMoreReading&userId=${userId}&skip=${skip}&take=${take}`)
            .then(response => response.json())
            .then(data => {
                loadMoreButton.style.display = "none";
                customHr.style.display = "none";

                data.forEach(book => {
                    let anchor = document.createElement('a');
                    anchor.className = 'UPI-listItem';
                    anchor.href = `/Manga/CurrentManga?id=${book.mangaID}`;

                    let img = document.createElement('img');
                    img.src = `/Images/GeneratedMangaImage/${book.photoPath}`;
                    img.alt = book.mangaName;
                    anchor.appendChild(img);

                    let span = document.createElement('span');
                    span.textContent = book.mangaName;
                    anchor.appendChild(span);

                    bookList.appendChild(anchor);
                });

                if (data.length < take) {
                    loadMoreButton.style.display = "none";
                    customHr.style.display = "none";
                } else {
                    loadMoreButton.style.display = "block";
                    customHr.style.display = "block";
                }

                skip += take;
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });
}

loadMoreReading();
function loadMoreCompleted() {
    let skip = 4; // Starting point for additional items
    const take = 100; // Number of additional items to take
    const loadMoreButton = document.getElementById("UPI-loadMoreCompleted");
    const customHr = document.querySelector('.UPI-custom-hr');
    const bookList = document.querySelector('.UPI-completedList');
    const userId = loadMoreButton.getAttribute('data-user-id');

    loadMoreButton.addEventListener("click", function () {
        adjustLayoutForExpandedList(loadMoreButton.closest('.UPI-listContainer'));
        fetch(`/UserInteractions/UserProfile?handler=LoadMoreCompleted&userId=${userId}&skip=${skip}&take=${take}`)
            .then(response => response.json())
            .then(data => {
                loadMoreButton.style.display = "none";
                customHr.style.display = "none";

                data.forEach(book => {
                    let anchor = document.createElement('a');
                    anchor.className = 'UPI-listItem';
                    anchor.href = `/Manga/CurrentManga?id=${book.mangaID}`;

                    let img = document.createElement('img');
                    img.src = `/Images/GeneratedMangaImage/${book.photoPath}`;
                    img.alt = book.mangaName;
                    anchor.appendChild(img);

                    let span = document.createElement('span');
                    span.textContent = book.mangaName;
                    anchor.appendChild(span);

                    bookList.appendChild(anchor);
                });

                if (data.length < take) {
                    loadMoreButton.style.display = "none";
                    customHr.style.display = "none";
                } else {
                    loadMoreButton.style.display = "block";
                    customHr.style.display = "block";
                }

                skip += take;
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });
}

loadMoreCompleted();

function adjustLayoutForExpandedList(currentListContainer) {
    // Expanding the current list container
    currentListContainer.classList.add('expanded-list');

    // Hiding other list containers
    let allListContainers = document.querySelectorAll('.UPI-container .UPI-listContainer');
    allListContainers.forEach(container => {
        if (container !== currentListContainer) {
            container.style.display = 'none';
        }
    });
}