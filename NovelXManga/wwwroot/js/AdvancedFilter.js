document.addEventListener("DOMContentLoaded", function () {
    function setupMultiStateCheckboxes(selector) {
        const checkboxes = document.querySelectorAll(`input[type="checkbox"][name="${selector}"]`);
        checkboxes.forEach((checkbox) => {
            checkbox.addEventListener('click', function (event) {
                let clickCount = Number(this.getAttribute('data-click-count')) || 0;
                clickCount++;

                this.setAttribute('data-click-count', clickCount);
            });
        });
    }

    setupMultiStateCheckboxes("selectedGenres");
});
function showHideGenres() {
    const sidebar = document.querySelector(".showHideGenre");
    sidebar.classList.toggle("showGenre");
}

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