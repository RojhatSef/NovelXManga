//Genre
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