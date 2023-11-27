function loadMoreReading() {
    let skip = 4; // Starting point for additional items
    const take = 100; // Number of additional items to take
    const loadMoreButton = document.getElementById("UPI-loadMoreReading");
    const customHr = document.querySelector('.UPI-custom-hr');
    const bookList = document.querySelector('.UPI-readingList');

    loadMoreButton.addEventListener("click", function () {
        fetch(`/UserInteractions/UserProfile?handler=LoadMoreReading&userId=${userId}&skip=${skip}&take=${take}`)
            .then(response => response.json())
            .then(data => {
                loadMoreButton.style.display = "none";
                customHr.style.display = "none";

                data.forEach(book => {
                    let listItem = document.createElement('div');
                    listItem.className = 'UPI-listItem';

                    let img = document.createElement('img');
                    img.src = `/Images/GeneratedMangaImage/${book.photoPath}`;
                    img.alt = book.mangaName;
                    listItem.appendChild(img);

                    let span = document.createElement('span');
                    span.textContent = book.mangaName;
                    listItem.appendChild(span);

                    bookList.appendChild(listItem);
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