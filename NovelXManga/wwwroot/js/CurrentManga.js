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
                    gridItem.className = 'CUS-GridItem';

                    let anchor = document.createElement('a');
                    anchor.href = `/CharacterPage/CurrentCharacter?id=${character.characterId}`;

                    let img = document.createElement('img');
                    img.className = 'GridImage';
                    img.src = `/Images/GeneratedCharacterImage/${character.photoPath}`;

                    anchor.appendChild(img);

                    let textDiv = document.createElement('div');
                    textDiv.className = 'CUS-MangaInputText';

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
document.addEventListener('DOMContentLoaded', function () {
    function toggleDropdown() {
        var dropdown = document.getElementById('listOptionsDropdown');
        var isDisplayed = dropdown.style.display === 'block';

        // Log to the console when the button is pressed
        console.log('Manage Lists Button Pressed');

        dropdown.style.display = isDisplayed ? 'none' : 'block';

        // If the dropdown is being shown, set a timeout to hide it
        if (!isDisplayed) {
            setTimeout(function () {
                if (dropdown.style.display === 'block') {
                    dropdown.style.display = 'none';
                }
            }, 800); // Adjust the delay as needed
        }
    }

    // Event listener for the manage lists button
    var manageListsButton = document.getElementById('manageListsButton');
    if (manageListsButton) {
        manageListsButton.addEventListener('click', toggleDropdown);
    }

    // Rest of your existing code...
});
function toggleDropdown() {
    var dropdown = document.getElementById("myDropdown");
    var dropbtn = document.querySelector('.CUS-dropbtn'); // Get the dropdown button

    // Toggle the 'show' class for the dropdown content
    dropdown.classList.toggle("CUS-show");

    // Toggle a 'hidden' class for the dropdown button
    dropbtn.classList.toggle("CUS-hidden");

    // If the dropdown is shown, add an event listener to the document
    if (dropdown.classList.contains("CUS-show")) {
        function handleClickOutside(event) {
            if (!dropdown.contains(event.target) && !event.target.matches('.CUS-dropbtn')) {
                // If the clicked element isn't the dropdown or the button, hide the dropdown content and show the button
                dropdown.classList.remove("CUS-show");
                dropbtn.classList.remove("CUS-hidden"); // Show the button again
                document.removeEventListener('click', handleClickOutside);
            }
        }
        document.addEventListener('click', handleClickOutside);
    } else {
        // If the dropdown is not shown (meaning it was just hidden), ensure the button is visible
        dropbtn.classList.remove("CUS-hidden");
    }
}

window.onclick = function (event) {
    if (!event.target.matches('.CUS-dropbtn')) {
        var dropdowns = document.getElementsByClassName("CUS-dropdown-content");
        var buttons = document.getElementsByClassName("CUS-dropbtn");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            var button = buttons[i]; // Assuming there's one button per dropdown
            if (openDropdown.classList.contains('CUS-show')) {
                openDropdown.classList.remove('CUS-show');
                button.classList.remove('CUS-hidden'); // Show the button when dropdown is hidden
            }
        }
    }
};