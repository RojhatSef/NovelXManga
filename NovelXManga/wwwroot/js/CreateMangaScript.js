document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById('genreForm');
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

    function loadState() {
        const savedPositive = localStorage.getItem('positiveSelectedGenres');
        if (savedPositive) {
            JSON.parse(savedPositive).forEach(id => positiveSelectedGenres.add(id));
        }
    }

    function initializeCheckboxes() {
        loadState();
        const checkboxes = document.querySelectorAll('.multi-state-checkbox');
        checkboxes.forEach((checkbox) => {
            const genreId = checkbox.value;
            const label = document.querySelector(`label[for="genre-${genreId}"]`);
            if (positiveSelectedGenres.has(genreId)) {
                checkbox.checked = true;
                label.classList.add("positive");
            } else {
                checkbox.checked = false;
            }
        });
    }

    function setupMultiStateCheckboxes() {
        const checkboxes = document.querySelectorAll('.multi-state-checkbox');
        checkboxes.forEach((checkbox) => {
            const label = document.querySelector(`label[for="${checkbox.id}"]`);
            checkbox.addEventListener('click', function (event) {
                const genreId = checkbox.value;
                if (positiveSelectedGenres.has(genreId)) {
                    checkbox.classList.remove("positive");
                    label.classList.remove("positive");
                    positiveSelectedGenres.delete(genreId);
                } else {
                    checkbox.classList.add("positive");
                    label.classList.add("positive");
                    positiveSelectedGenres.add(genreId);
                }
            });
        });
    }

    searchButton.addEventListener("click", function (e) {
        updateSelectedGenresInput(hiddenContainerPositive, positiveSelectedGenres);
        saveState();
        form.submit();
    });

    initializeCheckboxes();
    setupMultiStateCheckboxes();
});

//Positive TAG
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

    let allTagsData = Array.from(document.querySelectorAll('.MUS-tag-genre-item')).map(tag => {
        return {
            id: tag.querySelector('.MUS-hidden-checkbox').id,
            name: tag.querySelector('.MUS-tag-label').textContent,
            checked: tag.querySelector('.MUS-hidden-checkbox').checked
        };
    });

    let checkedState = {};
    allTagsData.forEach(tag => {
        checkedState[tag.id] = tag.checked;
    });

    function updateSelectedTagsDisplay() {
        let selectedTags = allTagsData.filter(tag => checkedState[tag.id]);
        selectedTagsList.innerHTML = '';
        selectedTags.forEach((tag, index) => {
            let span = document.createElement('span');
            span.textContent = tag.name;
            if (index !== 0) {
                let space = document.createTextNode(' ');
                selectedTagsList.appendChild(space);
            }
            span.className = 'selected-tag-item';
            span.addEventListener('click', function () {
                checkedState[tag.id] = false;
                document.getElementById(tag.id).checked = false;
                updateSelectedTagsDisplay();
                updateSelectedTagsInput();
            });
            selectedTagsList.appendChild(span);
        });
    }

    function updateSelectedTagsInput() {
        let selectedTagIds = allTagsData.filter(tag => checkedState[tag.id]).map(tag => parseInt(tag.id.split('-')[1]));
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
            checkedState[tag.id] = checkbox.checked;
            updateSelectedTagsDisplay();
            updateSelectedTagsInput();
        });
    }

    function displayTags(tagList) {
        tagDropdown.innerHTML = '';
        tagList.forEach(tag => {
            tag.checked = checkedState[tag.id];
            let tagItem = document.createElement('div');
            tagItem.className = 'MUS-tag-genre-item';
            //tagItem.onclick = function () {
            //    document.getElementById(tag.id).click();
            //};
            let checkbox = document.createElement('input');
            checkbox.type = 'checkbox';
            checkbox.className = 'MUS-hidden-checkbox';
            checkbox.id = tag.id;
            checkbox.name = 'SelectedTags';
            checkbox.value = parseInt(tag.id.split('-')[1]);
            checkbox.checked = tag.checked;
            addCheckboxEvent(checkbox, tag);
            let label = document.createElement('label');
            label.className = 'MUS-tag-label';
            label.htmlFor = tag.id;
            label.textContent = tag.name;
            tagItem.appendChild(checkbox);
            tagItem.appendChild(label);
            tagDropdown.appendChild(tagItem);
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