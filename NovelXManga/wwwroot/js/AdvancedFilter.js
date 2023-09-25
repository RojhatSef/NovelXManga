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

    /*  setupMultiStateCheckboxes("selectedTags");*/
    setupMultiStateCheckboxes("selectedGenres");
});
document.addEventListener("DOMContentLoaded", function () {
    const tagInput = document.getElementById('tagInput');
    const tagDropdown = document.getElementById('tagDropdown');

    tagInput.addEventListener('focus', function () {
        tagDropdown.style.display = 'block';
    });

    tagInput.addEventListener('blur', function () {
        tagDropdown.style.display = 'none';
    });
});

function showHideGenres() {
    const sidebar = document.querySelector(".showHideGenre");
    sidebar.classList.toggle("showGenre");
}

function toggleCheckbox(tagId) {
    const checkbox = document.getElementById(`tag-${tagId}`);
    checkbox.checked = !checkbox.checked;

    const tagItem = document.querySelector(`[onclick="toggleCheckbox('${tagId}')"]`);
    if (checkbox.checked) {
        tagItem.classList.add('selected');
    } else {
        tagItem.classList.remove('selected');
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const checkboxes = document.querySelectorAll('.MUS-hidden-checkbox');
    checkboxes.forEach(checkbox => {
        const tagItem = document.querySelector(`[onclick="toggleCheckbox('${checkbox.value}')"]`);
        if (checkbox.checked) {
            tagItem.classList.add('selected');
        }
    });
});