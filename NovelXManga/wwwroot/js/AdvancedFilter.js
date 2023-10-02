document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById('genreForm');
    const searchButton = document.getElementById('searchButton');
    const hiddenContainerPositive = document.getElementById('hiddenContainerPositive');
    const hiddenContainerNegative = document.getElementById('hiddenContainerNegative');
    const positiveSelectedGenres = new Set();
    const negativeSelectedGenres = new Set();

    function updateSelectedGenresInput(container, selectedGenres) {
        container.innerHTML = '';
        Array.from(selectedGenres).forEach(genreId => {
            let hiddenInput = document.createElement('input');
            hiddenInput.type = 'hidden';
            hiddenInput.name = container.id === 'hiddenContainerPositive' ? 'PositiveSelectedGenres' : 'NegativeSelectedGenres';
            hiddenInput.value = genreId;
            container.appendChild(hiddenInput);
        });
    }

    function saveState() {
        localStorage.setItem('positiveSelectedGenres', JSON.stringify(Array.from(positiveSelectedGenres)));
        localStorage.setItem('negativeSelectedGenres', JSON.stringify(Array.from(negativeSelectedGenres)));
    }

    function loadState() {
        const savedPositive = localStorage.getItem('positiveSelectedGenres');
        const savedNegative = localStorage.getItem('negativeSelectedGenres');

        if (savedPositive) {
            JSON.parse(savedPositive).forEach(id => positiveSelectedGenres.add(id));
        }

        if (savedNegative) {
            JSON.parse(savedNegative).forEach(id => negativeSelectedGenres.add(id));
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
                checkbox.setAttribute('data-click-count', 2);
                checkbox.classList.add("positive");
                label.classList.add("positive");
            } else if (negativeSelectedGenres.has(genreId)) {
                checkbox.checked = true;
                checkbox.setAttribute('data-click-count', 1);
                checkbox.classList.add("negative");
                label.classList.add("negative");
            } else {
                checkbox.checked = false;
                checkbox.setAttribute('data-click-count', 0);
            }
        });
    }

    function setupMultiStateCheckboxes() {
        const checkboxes = document.querySelectorAll('.multi-state-checkbox');
        checkboxes.forEach((checkbox) => {
            const label = document.querySelector(`label[for="${checkbox.id}"]`);
            const clickHandler = function (event) {
                const genreId = checkbox.value;
                let clickCount = Number(checkbox.getAttribute('data-click-count')) || 0;
                clickCount = (clickCount + 1) % 3;

                checkbox.setAttribute('data-click-count', clickCount);
                checkbox.classList.remove("positive");
                checkbox.classList.remove("negative");
                label.classList.remove("positive");
                label.classList.remove("negative");

                positiveSelectedGenres.delete(genreId);
                negativeSelectedGenres.delete(genreId);

                if (clickCount === 2) {
                    checkbox.classList.add("positive");
                    label.classList.add("positive");
                    positiveSelectedGenres.add(genreId);
                } else if (clickCount === 1) {
                    checkbox.classList.add("negative");
                    label.classList.add("negative");
                    negativeSelectedGenres.add(genreId);
                }
            };

            checkbox.addEventListener('click', clickHandler);
            label.addEventListener('click', clickHandler);
        });
    }

    searchButton.addEventListener("click", function (e) {
        e.preventDefault();
        updateSelectedGenresInput(hiddenContainerPositive, positiveSelectedGenres);
        updateSelectedGenresInput(hiddenContainerNegative, negativeSelectedGenres);
        saveState();
        form.submit();
    });

    initializeCheckboxes();
    setupMultiStateCheckboxes();
});

function showHideGenres() {
    const sidebar = document.querySelector(".showHideGenre");
    sidebar.classList.toggle("showGenre");
}
function initializePositiveTags() {
    // Your existing logic for positive tags here
}

function initializeNegativeTags() {
    // Your existing logic for negative tags here
}
//Positive TAG
document.addEventListener("DOMContentLoaded", function () {
    const tagInput = document.getElementById('tagInput');
    const tagDropdown = document.getElementById('tagDropdown');
    const selectedTagsList = document.getElementById('selectedTagsList');
    const hiddenContainer = document.getElementById('hiddenContainer');

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
            if (index !== 0) { // Add a space before the tag name, except for the first one
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

    document.getElementById('searchButton').addEventListener('click', function (e) {
        e.preventDefault();
        updateSelectedTagsInput();
        document.getElementById('AdvancedSearch').submit();
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
            tagItem.onclick = function () {
                document.getElementById(tag.id).click();
            };

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

    tagInput.addEventListener('input', function () {
        filterTags();
    });

    displayTags(allTagsData);
    updateSelectedTagsDisplay();
    updateSelectedTagsInput();
});

//Negative TAG
document.addEventListener("DOMContentLoaded", function () {
    const negativeTagInput = document.getElementById('negativeTagInput');
    const negativeTagDropdown = document.getElementById('negativeTagDropdown');
    const negativeSelectedTagsList = document.getElementById('negativeSelectedTagsList');
    const negativeHiddenContainer = document.getElementById('negativeHiddenContainer');

    let allNegativeTagsData = Array.from(document.querySelectorAll('.MUS-negative-tag-genre-item')).map(tag => {
        return {
            id: tag.querySelector('.MUS-hidden-checkbox').id,
            name: tag.querySelector('.MUS-negative-tag-label').textContent,
            checked: tag.querySelector('.MUS-hidden-checkbox').checked
        };
    });

    let negativeCheckedState = {};
    allNegativeTagsData.forEach(tag => {
        negativeCheckedState[tag.id] = tag.checked;
    });

    function updateNegativeSelectedTagsDisplay() {
        let selectedTags = allNegativeTagsData.filter(tag => negativeCheckedState[tag.id]);
        negativeSelectedTagsList.innerHTML = '';
        selectedTags.forEach((tag, index) => {
            let span = document.createElement('span');
            span.textContent = tag.name;
            if (index !== 0) {
                let space = document.createTextNode(' ');
                negativeSelectedTagsList.appendChild(space);
            }
            span.className = 'selected-tag-item';
            span.addEventListener('click', function () {
                negativeCheckedState[tag.id] = false;
                document.getElementById(tag.id).checked = false;
                updateNegativeSelectedTagsDisplay();
                updateNegativeSelectedTagsInput();
            });
            negativeSelectedTagsList.appendChild(span);
        });
    }

    function updateNegativeSelectedTagsInput() {
        let selectedTagIds = allNegativeTagsData.filter(tag => negativeCheckedState[tag.id]).map(tag => parseInt(tag.id.split('-')[2]));

        negativeHiddenContainer.innerHTML = '';
        selectedTagIds.forEach(tagId => {
            let hiddenInput = document.createElement('input');
            hiddenInput.type = 'hidden';
            hiddenInput.name = 'NegativeSelectedTags';
            hiddenInput.value = tagId;
            negativeHiddenContainer.appendChild(hiddenInput);
        });
    }

    document.getElementById('searchButton').addEventListener('click', function (e) {
        e.preventDefault();
        updateNegativeSelectedTagsInput();
        document.getElementById('AdvancedSearch').submit();
    });

    function addCheckboxEvent(checkbox, tag) {
        checkbox.addEventListener('change', function () {
            negativeCheckedState[tag.id] = checkbox.checked;
            updateNegativeSelectedTagsDisplay();
            updateNegativeSelectedTagsInput();
        });
    }

    function displayNegativeTags(tagList) {
        negativeTagDropdown.innerHTML = '';
        tagList.forEach(tag => {
            tag.checked = negativeCheckedState[tag.id];

            let tagItem = document.createElement('div');
            tagItem.className = 'MUS-negative-tag-genre-item';
            tagItem.onclick = function () {
                document.getElementById(tag.id).click();
            };

            let checkbox = document.createElement('input');
            checkbox.type = 'checkbox';
            checkbox.className = 'MUS-hidden-checkbox';
            checkbox.id = tag.id;

            checkbox.value = parseInt(tag.id.split('-')[2]);

            checkbox.checked = tag.checked;

            addCheckboxEvent(checkbox, tag);

            let label = document.createElement('label');
            label.className = 'MUS-negative-tag-label';
            label.htmlFor = tag.id;
            label.textContent = tag.name;

            tagItem.appendChild(checkbox);
            tagItem.appendChild(label);

            negativeTagDropdown.appendChild(tagItem);
        });
    }

    function filterNegativeTags() {
        let query = negativeTagInput.value.toLowerCase();
        let filteredTags = allNegativeTagsData.filter(tag => tag.name.toLowerCase().includes(query));
        displayNegativeTags(filteredTags);
    }

    negativeTagInput.addEventListener('input', function () {
        filterNegativeTags();
    });

    displayNegativeTags(allNegativeTagsData);
    updateNegativeSelectedTagsDisplay();
    updateNegativeSelectedTagsInput();
});