document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById('create-manga-form');
    const searchButton = document.getElementById('CreateManga');
    const hiddenContainerPositive = document.getElementById('hiddenContainerPositive');
    const positiveSelectedGenres = new Set();

    function updateSelectedGenresInput(container, selectedGenres) {
        container.innerHTML = '';
        Array.from(selectedGenres).forEach(genreId => {
            let hiddenInput = document.createElement('input');
            hiddenInput.type = 'hidden';
            hiddenInput.name = 'PositiveSelectedGenres';
            hiddenInput.value = genreId;
            container.appendChild(hiddenInput);
        });
    }

    function saveState() {
        localStorage.setItem('positiveSelectedGenres', JSON.stringify(Array.from(positiveSelectedGenres)));
    }

    function clearLocalStorage() {
        localStorage.removeItem('positiveSelectedGenres');  // Clear any saved genre selections from previous sessions
    }

    function loadState() {
        // After clearing, check if there is still relevant data (e.g., if cleared just before saving new state)
        const savedPositive = localStorage.getItem('positiveSelectedGenres');
        if (savedPositive) {
            const savedGenres = new Set(JSON.parse(savedPositive));
            document.querySelectorAll('.multi-state-checkbox').forEach(checkbox => {
                const genreId = checkbox.value;
                const label = document.querySelector(`label[for="genre-${genreId}"]`);
                if (savedGenres.has(genreId)) {
                    checkbox.checked = true;
                    label.classList.add("positive");
                    positiveSelectedGenres.add(genreId);
                } else {
                    checkbox.checked = false;
                    label.classList.remove("positive");
                    positiveSelectedGenres.delete(genreId);
                }
            });
        }
    }

    function initializeCheckboxes() {
        // Clear any old data before initializing checkboxes based on server-rendered state
        clearLocalStorage();
        const checkboxes = document.querySelectorAll('.multi-state-checkbox');
        checkboxes.forEach((checkbox) => {
            const genreId = checkbox.value;
            const label = document.querySelector(`label[for="genre-${genreId}"]`);
            if (checkbox.checked) {
                label.classList.add("positive");
                positiveSelectedGenres.add(genreId);
            } else {
                label.classList.remove("positive");
                positiveSelectedGenres.delete(genreId);
            }
        });
    }

    function setupMultiStateCheckboxes() {
        const checkboxes = document.querySelectorAll('.multi-state-checkbox');
        checkboxes.forEach((checkbox) => {
            const label = document.querySelector(`label[for="${checkbox.id}"]`);
            checkbox.addEventListener('click', function () {
                const genreId = checkbox.value;
                if (checkbox.checked) {
                    label.classList.add("positive");
                    positiveSelectedGenres.add(genreId);
                } else {
                    label.classList.remove("positive");
                    positiveSelectedGenres.delete(genreId);
                }
            });
        });
    }

    searchButton.addEventListener("click", function () {
        updateSelectedGenresInput(hiddenContainerPositive, positiveSelectedGenres);
        saveState();
        form.submit();
    });

    initializeCheckboxes();
    setupMultiStateCheckboxes();
});

//Positive TAG Create Manga
document.addEventListener("DOMContentLoaded", function () {
    const tagInput = document.getElementById('tagInput');
    const tagDropdown = document.getElementById('tagDropdown');
    const selectedTagsList = document.getElementById('selectedTagsList');
    const hiddenContainer = document.getElementById('hiddenContainer');
    let dropdownOpen = false;
    // Manual dropdown control
    tagDropdown.addEventListener('focusin', function () {
        dropdownOpen = true;
        tagDropdown.style.display = 'block';
    });
    tagDropdown.addEventListener('focusout', function () {
        dropdownOpen = false;
        setTimeout(function () {
            if (!dropdownOpen) {
                tagDropdown.style.display = 'none';
            }
        }, 100);
    });
    tagInput.addEventListener('focus', function () {
        dropdownOpen = true;
        tagDropdown.style.display = 'block';
    });
    let allTagsData = Array.from(document.querySelectorAll('.MUS-tag-genre-item-tag')).map(tag => {
        return {
            id: tag.querySelector('.MUS-hidden-checkbox').id,
            name: tag.querySelector('.MUS-tag-label').textContent,
            checked: tag.querySelector('.MUS-hidden-checkbox').checked
        };
    });
    let checkedTagsState = {};
    allTagsData.forEach(tag => {
        checkedTagsState[tag.id] = tag.checked;
    });
    function updateSelectedTagsDisplay() {
        // Clear the current display to rebuild it
        selectedTagsList.innerHTML = '';

        // Filter out only selected tags based on the checked state
        let selectedTags = allTagsData.filter(tag => checkedTagsState[tag.id]);

        // Create a new Set to track which tags have been added
        let addedTags = new Set();

        // Iterate over selected tags to add them to the display
        selectedTags.forEach(tag => {
            // Check if the tag has already been added to prevent duplicates
            if (!addedTags.has(tag.id)) {
                // Create a span element for the tag
                let span = document.createElement('span');
                span.textContent = tag.name;
                span.className = 'selected-tag-item';
                // Add a click listener to remove the tag on click
                span.addEventListener('click', function () {
                    // Update the checked state and UI
                    checkedTagsState[tag.id] = false;
                    document.getElementById(tag.id).checked = false;
                    updateSelectedTagsDisplay();
                    updateSelectedTagsInput();
                });

                // Append the tag span to the list
                selectedTagsList.appendChild(span);

                // If not the first item, add a space for visual separation
                if (selectedTagsList.children.length > 1) {
                    selectedTagsList.insertBefore(document.createTextNode(' '), span);
                }

                // Mark the tag as added to avoid duplication
                addedTags.add(tag.id);
            }
        });
    }

    function updateSelectedTagsInput() {
        let selectedTagIds = allTagsData.filter(tag => checkedTagsState[tag.id]).map(tag => parseInt(tag.id.split('-')[1]));
        hiddenContainer.innerHTML = '';
        selectedTagIds.forEach(tagId => {
            let hiddenInput = document.createElement('input');
            hiddenInput.type = 'hidden';
            hiddenInput.name = 'JsSelectedTags';
            hiddenInput.value = tagId;
            hiddenContainer.appendChild(hiddenInput);
        });
    }

    document.getElementById('CreateManga').addEventListener('click', function (e) {
        e.preventDefault();
        updateSelectedTagsInput();
        document.getElementById('create-manga-form').submit();
    });

    function addCheckboxEvent(checkbox, tag) {
        checkbox.addEventListener('change', function () {
            checkedTagsState[tag.id] = checkbox.checked;
            updateSelectedTagsDisplay();
            updateSelectedTagsInput();
        });
    }
    function displayTags(tagList) {
        tagDropdown.innerHTML = '';
        tagList.forEach(tag => {
            let tagItem = document.createElement('div');
            tagItem.className = 'MUS-tag-genre-item-tag';
            let checkbox = document.createElement('input');
            checkbox.type = 'checkbox';
            checkbox.className = 'MUS-hidden-checkbox';
            checkbox.id = tag.id;
            checkbox.name = 'SelectedTags';
            checkbox.value = parseInt(tag.id.split('-')[1]);
            checkbox.checked = checkedTagsState[tag.id];

            let label = document.createElement('label');
            label.className = 'MUS-tag-label';
            label.htmlFor = tag.id;
            label.textContent = tag.name;

            tagItem.appendChild(checkbox);
            tagItem.appendChild(label);
            tagDropdown.appendChild(tagItem);

            addCheckboxEvent(checkbox, tag);
        });
    }
    function filterTags() {
        let query = tagInput.value.toLowerCase();
        let filteredTags = allTagsData.filter(tag => tag.name.toLowerCase().includes(query));
        displayTags(filteredTags);
    }
    document.addEventListener('click', function (e) {
        if (!tagDropdown.contains(e.target) && e.target !== tagInput) {
            dropdownOpen = false;
            tagDropdown.style.display = 'none';
        }
    });
    tagInput.addEventListener('input', function () {
        filterTags();
    });
    displayTags(allTagsData);
    updateSelectedTagsDisplay();
    updateSelectedTagsInput();
});

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
    document.getElementById('CreateManga').addEventListener('click', function (e) {
        e.preventDefault();
        updateSelectedTagsInput();
        document.getElementById('create-manga-form').submit();
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
    document.getElementById('CreateManga').addEventListener('click', function (e) {
        e.preventDefault();

        updateSelectedTagsInput();
        document.getElementById('create-manga-form').submit();
    });

    displayRecommendedManga(allRecommendedMangaData);
    updateSelectedRecommendedMangaDisplay();
    updateSelectedRecommendedMangaInput();
});

//checks if ending year is less than release year