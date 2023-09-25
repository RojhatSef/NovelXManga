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

//New
document.addEventListener("DOMContentLoaded", function () {
    // Initialize constants and tag data
    const tagInput = document.getElementById('tagInput');
    const tagDropdown = document.getElementById('tagDropdown');
    const selectedTagsList = document.getElementById('selectedTagsList');
    let allTagsData = Array.from(document.querySelectorAll('.MUS-tag-genre-item')).map(tag => {
        return {
            id: tag.querySelector('.MUS-hidden-checkbox').id,
            name: tag.querySelector('.MUS-tag-label').textContent,
            checked: tag.querySelector('.MUS-hidden-checkbox').checked
        };
    });

    // Function to update the display of selected tags
    function updateSelectedTagsDisplay() {
        let selectedTags = [];
        allTagsData.forEach(function (tag) {
            if (tag.checked) {
                selectedTags.push(tag.name);
            }
        });
        selectedTagsList.textContent = selectedTags.join(", ");
    }

    // Function to add click events to checkboxes
    function addCheckboxEvent(checkbox, tag) {
        checkbox.addEventListener('change', function () {
            tag.checked = checkbox.checked;
            updateSelectedTagsDisplay();
        });
    }

    // Function to display tags based on a list
    function displayTags(tagList) {
        tagDropdown.innerHTML = '';

        tagList.forEach(tag => {
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
            checkbox.value = tag.id;
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

    // Function to filter and display tags
    function filterTags() {
        let query = tagInput.value.toLowerCase();
        let filteredTags = allTagsData.filter(tag => tag.name.toLowerCase().includes(query));
        displayTags(filteredTags);
    }

    // Event listener for filtering tags on input change
    tagInput.addEventListener('input', function () {
        filterTags();
    });

    // Initial display of tags
    displayTags(allTagsData);
    updateSelectedTagsDisplay();
});