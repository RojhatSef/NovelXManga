﻿(function () {
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
