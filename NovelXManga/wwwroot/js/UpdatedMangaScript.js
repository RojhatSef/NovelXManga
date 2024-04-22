//related manga

document.addEventListener("DOMContentLoaded", function () {
    const relatedMangaInput = document.getElementById('relatedMangaInput');
    const relatedMangaDropdown = document.getElementById('relatedMangaDropdown');
    const selectedRelatedMangaList = document.getElementById('selectedRelatedMangaList');
    const hiddenContainerForRelatedManga = document.getElementById('hiddenContainerForRelatedManga');
    let dropdownOpen = false;

    // Manual dropdown control
    relatedMangaInput.addEventListener('focusin', function () {
        dropdownOpen = true;
        relatedMangaDropdown.style.display = 'block';
    });

    relatedMangaDropdown.addEventListener('focusout', function () {
        dropdownOpen = false;
        setTimeout(function () {
            if (!dropdownOpen) {
                relatedMangaDropdown.style.display = 'none';
            }
        }, 100);
    });

    relatedMangaInput.addEventListener('focus', function () {
        dropdownOpen = true;
        relatedMangaDropdown.style.display = 'block';
    });

    let allRelatedMangaData = Array.from(document.querySelectorAll('#RelatedMangaItems')).map(allRelatedMangaData => {
        return {
            id: allRelatedMangaData.querySelector('.MUS-hidden-checkbox').id,
            name: allRelatedMangaData.querySelector('.MUS-tag-label').textContent,
            checked: allRelatedMangaData.querySelector('.MUS-hidden-checkbox').checked
        };
    });

    let checkedStateRelatedManga = {};
    allRelatedMangaData.forEach(manga => {
        checkedStateRelatedManga[manga.id] = manga.checked;
    });
    function updateSelectedRelatedMangaDisplay() {
        selectedRelatedMangaList.innerHTML = ''; // Clear existing display first
        // Iterate over each manga, checking if it's selected
        allRelatedMangaData.forEach(manga => {
            if (checkedStateRelatedManga[manga.id]) { // Checking if manga is selected
                const span = document.createElement('span');
                span.textContent = manga.name;
                span.className = 'selected-related-manga-item';
                span.addEventListener('click', () => {
                    // Toggle selection off when clicked
                    const isSelected = !checkedStateRelatedManga[manga.id]; // Toggle and capture new state
                    checkedStateRelatedManga[manga.id] = isSelected; // Update the state
                    const checkbox = document.getElementById(manga.id); // Get the checkbox
                    if (checkbox) { // Safety check in case the element is not found
                        checkbox.checked = isSelected; // Align checkbox state
                    }
                    updateSelectedRelatedMangaDisplay();
                    updateSelectedRelatedMangaInput();
                });
                selectedRelatedMangaList.appendChild(span);
            }
        });
    }

    function displayRelatedManga() {
        relatedMangaDropdown.innerHTML = ''; // Clear existing items
        allRelatedMangaData.forEach(manga => {
            const div = document.createElement('div');
            div.textContent = manga.name;
            div.className = 'related-manga-item';
            div.onclick = () => {
                checkedStateRelatedManga[manga.id] = !checkedStateRelatedManga[manga.id]; // Toggle selection state
                updateSelectedRelatedMangaDisplay(); // Update UI
                updateSelectedRelatedMangaInput(); // Update hidden inputs
            };
            relatedMangaDropdown.appendChild(div);
        });
    }

    relatedMangaInput.addEventListener('input', () => {
        let searchText = relatedMangaInput.value.toLowerCase();
        let filteredManga = allRelatedMangaData.filter(manga => manga.name.toLowerCase().includes(searchText));
        displayRelatedManga(filteredManga);
    });

    // Close the dropdown if clicking outside
    document.addEventListener('click', (e) => {
        if (!relatedMangaDropdown.contains(e.target) && e.target !== relatedMangaInput) {
            dropdownOpen = false;
            relatedMangaDropdown.style.display = 'none';
        }
    });
    function updateSelectedRelatedMangaInput() {
        hiddenContainerForRelatedManga.innerHTML = ''; // Clear any previously added inputs
        Object.keys(checkedStateRelatedManga).forEach(id => {
            if (checkedStateRelatedManga[id]) { // If the manga is selected
                const input = document.createElement('input');
                input.type = 'hidden';
                input.name = 'SelectedRelatedMangaIds';
                input.value = id.replace('related-manga-', ''); // Remove prefix if it exists
                hiddenContainerForRelatedManga.appendChild(input);
            }
        });
    }

    relatedMangaInput.addEventListener('focus', () => {
        dropdownOpen = true;
        relatedMangaDropdown.style.display = 'block';
    });
    document.getElementById('UpdateManga').addEventListener('click', function (e) {
        /* e.preventDefault();*/
        updateSelectedTagsInput();
        document.getElementById('create-manga-update').submit();
    });

    displayRelatedManga(allRelatedMangaData);
    updateSelectedRelatedMangaDisplay();
    updateSelectedRelatedMangaInput();
});

//recommended manga
document.addEventListener("DOMContentLoaded", function () {
    const recommendedMangaInput = document.getElementById('recommendedMangaInput');
    const recommendedMangaDropdown = document.getElementById('recommendedMangaDropdown');
    const selectedRecommendedMangaList = document.getElementById('selectedRecommendedMangaList');
    const hiddenContainerForRecommendedManga = document.getElementById('hiddenContainerForRecommendedManga');
    let dropdownOpen = false;

    // Manual dropdown control
    recommendedMangaInput.addEventListener('focusin', function () {
        dropdownOpen = true;
        recommendedMangaDropdown.style.display = 'block';
    });

    recommendedMangaDropdown.addEventListener('focusout', function () {
        dropdownOpen = false;
        setTimeout(function () {
            if (!dropdownOpen) {
                recommendedMangaDropdown.style.display = 'none';
            }
        }, 100);
    });

    recommendedMangaInput.addEventListener('focus', function () {
        dropdownOpen = true;
        recommendedMangaDropdown.style.display = 'block';
    });

    let allRecommendedMangaData = Array.from(document.querySelectorAll('#RecommendedMangaItems')).map(recommendedMangaData => {
        return {
            id: recommendedMangaData.querySelector('.MUS-hidden-checkbox').id,
            name: recommendedMangaData.querySelector('.MUS-tag-label').textContent,
            checked: recommendedMangaData.querySelector('.MUS-hidden-checkbox').checked
        };
    });

    let checkedStateRecommendedManga = {};
    allRecommendedMangaData.forEach(manga => {
        checkedStateRecommendedManga[manga.id] = manga.checked;
    });
    function updateSelectedRecommendedMangaDisplay() {
        selectedRecommendedMangaList.innerHTML = '';
        allRecommendedMangaData.forEach(manga => {
            if (checkedStateRecommendedManga[manga.id]) {
                const span = document.createElement('span');
                span.textContent = manga.name;
                span.className = 'selected-recommended-manga-item';
                span.addEventListener('click', () => {
                    const isSelected = !checkedStateRecommendedManga[manga.id];
                    checkedStateRecommendedManga[manga.id] = isSelected;
                    const checkbox = document.getElementById(manga.id);
                    if (checkbox) {
                        checkbox.checked = isSelected;
                    }
                    updateSelectedRecommendedMangaDisplay();
                    updateSelectedRecommendedMangaInput();
                });
                selectedRecommendedMangaList.appendChild(span);
            }
        });
    }

    function displayRecommendedManga() {
        recommendedMangaDropdown.innerHTML = '';
        allRecommendedMangaData.forEach(manga => {
            const div = document.createElement('div');
            div.textContent = manga.name;
            div.className = 'recommended-manga-item';
            div.onclick = () => {
                checkedStateRecommendedManga[manga.id] = !checkedStateRecommendedManga[manga.id];
                updateSelectedRecommendedMangaDisplay();
                updateSelectedRecommendedMangaInput();
            };
            recommendedMangaDropdown.appendChild(div);
        });
    }

    recommendedMangaInput.addEventListener('input', () => {
        let searchText = recommendedMangaInput.value.toLowerCase();
        let filteredManga = allRecommendedMangaData.filter(manga => manga.name.toLowerCase().includes(searchText));
        displayRecommendedManga(filteredManga);
    });

    document.addEventListener('click', (e) => {
        if (!recommendedMangaDropdown.contains(e.target) && e.target !== recommendedMangaInput) {
            dropdownOpen = false;
            recommendedMangaDropdown.style.display = 'none';
        }
    });
    function updateSelectedRecommendedMangaInput() {
        hiddenContainerForRecommendedManga.innerHTML = '';
        Object.keys(checkedStateRecommendedManga).forEach(id => {
            if (checkedStateRecommendedManga[id]) {
                const input = document.createElement('input');
                input.type = 'hidden';
                input.name = 'SelectedRecommendedMangaIds';
                input.value = id.replace('recommended-manga-', '');
                hiddenContainerForRecommendedManga.appendChild(input);
            }
        });
    }

    recommendedMangaInput.addEventListener('focus', () => {
        dropdownOpen = true;
        recommendedMangaDropdown.style.display = 'block';
    });
    document.getElementById('UpdateManga').addEventListener('click', function (e) {
        /*     e.preventDefault();*/
        // Assuming updateSelectedTagsInput function exists and is relevant for recommended manga as well
        updateSelectedTagsInput();
        document.getElementById('create-manga-update').submit();
    });

    displayRecommendedManga(allRecommendedMangaData);
    updateSelectedRecommendedMangaDisplay();
    updateSelectedRecommendedMangaInput();
});