let interval;
let index = 0;

function startRotation() {
    if (interval) clearInterval(interval);

    interval = setInterval(() => {
        index = index % books.length;
        updateImage(books[index].PhotoPath);
        index++;
    }, 6000);
}

function updateImage(photoPath) {
    const existingImage = document.querySelector('#bookDetails img');
    if (existingImage) existingImage.src = `/Images/GeneratedMangaImage/${photoPath}`;
    else {
        let imgElement = document.createElement('img');
        imgElement.src = `/Images/GeneratedMangaImage/${photoPath}`;
        imgElement.alt = "Manga Cover";
        imgElement.classList.add("BoxImageLogiRegi", "SpecialBox", "fade");
        document.getElementById('bookDetails').appendChild(imgElement);
    }

    // Update the radio button selection
    const radioButtons = document.querySelectorAll('.radio-buttons-container input[type="radio"]');
    radioButtons.forEach((radio, i) => {
        radio.checked = (i === index);
    });
}

function addRadioButtons() {
    const radioButtonContainer = document.createElement('div');
    radioButtonContainer.classList.add('radio-buttons-container');
    for (let i = 0; i < 10; i++) {
        let radio = document.createElement('input');
        radio.type = 'radio';
        radio.name = 'bookRadio';
        radio.id = 'bookRadio' + i;
        radio.setAttribute('data-index', i);
        radio.onclick = function () { switchBook(this); };

        // Check the radio corresponding to the current book index
        if (i === index) {
            radio.checked = true;
        }

        radioButtonContainer.appendChild(radio);
    }
    document.getElementById('bookDetails').prepend(radioButtonContainer);
    console.log(`Radio buttons added.`);
}

function switchBook(radioButton) {
    let newIndex = parseInt(radioButton.getAttribute('data-index'));
    if (newIndex < books.length) {
        index = newIndex;
        updateImage(books[index].PhotoPath);  // ensure the radio button and image are synced

        // Stop the rotation momentarily
        clearInterval(interval);

        // Restart the rotation after the delay
        setTimeout(() => {
            index++; // Increment here so it loads the next book when rotation restarts
            startRotation();
        }, 6000);
    }
}

// Start the rotation on page load
window.onload = function () {
    startRotation();
    // Set the initial radio button to checked
    updateImage(books[0].PhotoPath);
}