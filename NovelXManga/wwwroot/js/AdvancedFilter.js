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
    const hiddenContainer = document.getElementById('hiddenContainer'); // Added

    let allTagsData = Array.from(document.querySelectorAll('.MUS-tag-genre-item')).map(tag => {
        return {
            id: tag.querySelector('.MUS-hidden-checkbox').id,
            name: tag.querySelector('.MUS-tag-label').textContent,
            checked: tag.querySelector('.MUS-hidden-checkbox').checked
        };
    });

    // Keep the separate state for checked tags
    let checkedState = {};
    allTagsData.forEach(tag => {
        checkedState[tag.id] = tag.checked;
    });

    function updateSelectedTagsDisplay() {
        let selectedTags = allTagsData.filter(tag => checkedState[tag.id]).map(tag => tag.name);
        selectedTagsList.textContent = selectedTags.join(", ");
    }

    function updateSelectedTagsInput() {
        let selectedTagIds = allTagsData.filter(tag => checkedState[tag.id]).map(tag => parseInt(tag.id.split('-')[1]));

        hiddenContainer.innerHTML = ''; // Clear previous inputs
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
            tag.checked = checkedState[tag.id]; // Update checked state from the separate state object

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

////old
//document.addEventListener("DOMContentLoaded", function () {
//    const tagInput = document.getElementById('tagInput');
//    const tagDropdown = document.getElementById('tagDropdown');
//    const selectedTagsList = document.getElementById('selectedTagsList');
//    let allTagsData = Array.from(document.querySelectorAll('.MUS-tag-genre-item')).map(tag => {
//        return {
//            id: tag.querySelector('.MUS-hidden-checkbox').id,
//            name: tag.querySelector('.MUS-tag-label').textContent,
//            checked: tag.querySelector('.MUS-hidden-checkbox').checked
//        };
//    });

//    function updateSelectedTagsDisplay() {
//        let selectedTags = [];
//        allTagsData.forEach(function (tag) {
//            if (tag.checked) {
//                selectedTags.push(tag.name);
//            }
//        });
//        selectedTagsList.textContent = selectedTags.join(", ");
//    }

//    function updateSelectedTagsInput() {
//        let selectedTagIds = [];
//        allTagsData.forEach(function (tag) {
//            if (tag.checked) {
//                selectedTagIds.push(parseInt(tag.id.split('-')[1]));
//            }
//        });
//        document.getElementById('selectedTagsHidden').value = selectedTagIds.join(',');
//        document.getElementById('selectedTagsHidden').name = 'JsSelectedTags';
//    }

//    document.querySelectorAll('input[type="checkbox"][name="SelectedTags"]').forEach((checkbox) => {
//        checkbox.addEventListener('change', function (event) {
//            updateSelectedTagsInput();
//        });
//    });

//    document.getElementById('searchButton').addEventListener('click', function (e) {
//        updateSelectedTagsInput();
//        document.getElementById('AdvancedSearch').submit();
//    });

//    function addCheckboxEvent(checkbox, tag) {
//        checkbox.addEventListener('change', function () {
//            tag.checked = checkbox.checked;
//            updateSelectedTagsDisplay();
//        });
//    }

//    function displayTags(tagList) {
//        tagDropdown.innerHTML = '';
//        tagList.forEach(tag => {
//            let tagItem = document.createElement('div');
//            tagItem.className = 'MUS-tag-genre-item';
//            tagItem.onclick = function () {
//                document.getElementById(tag.id).click();
//            };

//            let checkbox = document.createElement('input');
//            checkbox.type = 'checkbox';
//            checkbox.className = 'MUS-hidden-checkbox';
//            checkbox.id = tag.id;
//            checkbox.name = 'SelectedTags';
//            checkbox.value = parseInt(tag.id.split('-')[1]);
//            checkbox.checked = tag.checked;
//            addCheckboxEvent(checkbox, tag);

//            let label = document.createElement('label');
//            label.className = 'MUS-tag-label';
//            label.htmlFor = tag.id;
//            label.textContent = tag.name;

//            tagItem.appendChild(checkbox);
//            tagItem.appendChild(label);

//            tagDropdown.appendChild(tagItem);
//        });
//    }

//    function filterTags() {
//        let query = tagInput.value.toLowerCase();
//        let filteredTags = allTagsData.filter(tag => tag.name.toLowerCase().includes(query));
//        displayTags(filteredTags);
//    }

//    tagInput.addEventListener('input', function () {
//        filterTags();
//    });

//    displayTags(allTagsData);
//    updateSelectedTagsDisplay();
//    updateSelectedTagsInput();
//});