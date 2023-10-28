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
function loadMoreCharacters() {
    let skip = 5;
    const take = 100;
    const loadMoreButton = document.getElementById("loadMoreCharacters");
    const customHr = document.querySelector('.CUS-custom-hr');
    const gridWrapper = document.querySelector('.GridWrapper');

    loadMoreButton.addEventListener("click", function () {
        const mangaId = this.getAttribute("data-manga-id");

        fetch(`/Manga/CurrentManga?handler=LoadMoreCharacters&mangaId=${mangaId}&skip=${skip}&take=${take}`)
            .then(response => response.json())
            .then(data => {
                loadMoreButton.style.display = "none";
                customHr.style.display = "none";

                data.forEach(character => {
                    let gridItem = document.createElement('div');
                    gridItem.className = 'GridItem';

                    let anchor = document.createElement('a');
                    anchor.href = `/CharacterPage/CurrentCharacter?id=${character.characterId}`;

                    let img = document.createElement('img');
                    img.className = 'GridImage';
                    img.src = `/Images/GeneratedCharacterImage/${character.photoPath}`;

                    anchor.appendChild(img);

                    let textDiv = document.createElement('div');
                    textDiv.className = 'MangaInputText';

                    let p = document.createElement('p');
                    p.textContent = character.characterName;

                    textDiv.appendChild(p);
                    anchor.appendChild(textDiv);
                    gridItem.appendChild(anchor);
                    gridWrapper.appendChild(gridItem);
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

loadMoreCharacters();
document.addEventListener("DOMContentLoaded", function () {
    console.log("Console is available.");

    // Make sure mangaId is defined and correctly set
    let mangaId = document.querySelector("#your-html-element-for-mangaId").value;

    fetch(`/Manga/CurrentManga?handler=AdditionalData&mangaId=${mangaId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Network response was not ok");
            }
            return response.json();
        })
        .then(data => {
            // Append genres
            const genreContainer = document.querySelector(".CUS-CurrentMangaTagsContainer");
            data.genres.forEach(genre => {
                let labelClass = genre.isWarning ? 'CUS-CustomWarningLabel' : '';
                let label = `<label class="${labelClass}">${genre.name}</label>`;
                genreContainer.innerHTML += label;
            });

            // Append tags
            const tagContainer = document.querySelector(".CUS-CurrentMangaTagsContainer");
            data.tags.forEach(tag => {
                let labelClass = tag.isWarning ? 'CUS-CustomHintLabel' : '';
                let label = `<label class="${labelClass}">${tag.name}</label>`;
                tagContainer.innerHTML += label;
            });
            console.log("Page loaded successfully.");
        })
        .catch(error => {
            console.error("Fetch error: ", error);
        });
});