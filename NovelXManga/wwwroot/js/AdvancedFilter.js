document.addEventListener("DOMContentLoaded", function () {
    function setupMultiStateCheckboxes(selector) {
        const checkboxes = document.querySelectorAll(selector);
        checkboxes.forEach((checkbox) => {
            checkbox.addEventListener('click', function (event) {
                let clickCount = Number(this.getAttribute('data-click-count')) || 0;
                clickCount++;
                if (clickCount === 3) clickCount = 0;
                switch (clickCount) {
                    case 0:
                        this.checked = false;
                        this.nextElementSibling.style.color = "black";
                        break;
                    case 1:
                        this.checked = true;
                        this.nextElementSibling.style.color = "green";
                        break;
                    case 2:
                        this.checked = false;
                        this.nextElementSibling.style.color = "red";
                        break;
                }
                this.setAttribute('data-click-count', clickCount);
            });
        });
    }

    setupMultiStateCheckboxes('#allTags input[type="checkbox"]');

    document.getElementById('tagSearch').addEventListener('input', function () {
        const searchString = this.value.toLowerCase();
        const allTags = document.querySelectorAll('.tagItem');
        allTags.forEach(tag => {
            const tagLabel = tag.querySelector('label').innerText.toLowerCase();
            tag.style.display = tagLabel.includes(searchString) ? 'block' : 'none';
        });
    });

    document.getElementById('allTags').addEventListener('change', function (e) {
        if (e.target.tagName.toLowerCase() === 'input') {
            const tagId = e.target.getAttribute('id').replace('tag-', '');
            const tagName = e.target.nextElementSibling.innerText;
            if (e.target.checked) {
                const selectedTag = `<div class="selectedTag" data-tag-id="${tagId}">${tagName}<button class="removeTag">X</button></div>`;
                document.getElementById('selectedTags').innerHTML += selectedTag;
            } else {
                const selectedTag = document.querySelector(`.selectedTag[data-tag-id="${tagId}"]`);
                if (selectedTag) {
                    selectedTag.remove();
                }
            }
        }
    });