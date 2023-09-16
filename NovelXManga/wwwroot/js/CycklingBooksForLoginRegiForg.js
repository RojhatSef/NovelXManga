let interval;
let index = 0;

function startRotation() {
    if (interval) clearInterval(interval);

    interval = setInterval(() => {
        index = index % books.length;
        updateDetails(books[index]);
        index++;
    }, 6000);
}

function updateDetails(book) {
    // Update the image
    const existingImage = document.querySelector('#bookDetails img');
    if (existingImage) existingImage.src = `/Images/GeneratedMangaImage/${book.PhotoPath}`;

    // Update the overall score
    let scoreContainer = document.querySelector('.LoginScore');

    if (book.OverAllBookScore) {
        scoreContainer.style.display = ''; // Show the container
        scoreContainer.innerHTML = `<span>${book.OverAllBookScore} <i class="fas fa-star active colorstarCurrent"></i></span>`;
    } else {
        scoreContainer.style.display = 'none'; // Hide the container
        scoreContainer.innerHTML = '';
    }

    // Update genres
    let genreContainer = document.querySelector('.LoginGenres');
    genreContainer.innerHTML = '';
    if (book.GenresModels) {
        book.GenresModels.forEach(genre => {
            genreContainer.innerHTML += `<span>${genre.GenreName}</span>`;
        });
    }

    // Update tags
    let tagContainer = document.querySelector('.LoginTags');
    tagContainer.innerHTML = '';
    if (book.TagsModels) {
        book.TagsModels.forEach(tag => {
            tagContainer.innerHTML += `<span>${tag.TagName}</span>`;
        });
    }

    // Update authors
    let authorContainer = document.querySelector('.LoginAuthor');
    authorContainer.innerHTML = '';
    if (book.Authormodels) {
        book.Authormodels.forEach(author => {
            if (author.FirstName && author.LastName) {
                authorContainer.innerHTML += `<span>${author.FirstName} ${author.LastName}</span>`;
            } else if (author.FirstName) {
                authorContainer.innerHTML += `<span>${author.FirstName}</span>`;
            } else if (author.LastName) {
                authorContainer.innerHTML += `<span>${author.LastName}</span>`;
            }
        });
    }

    // Update artists
    let artistContainer = document.querySelector('.LoginArtist');
    artistContainer.innerHTML = '';
    if (book.ArtistModels) {
        book.ArtistModels.forEach(artist => {
            let displayArtist = true;
            if (book.Authormodels) {
                book.Authormodels.forEach(author => {
                    if (author.FirstName == artist.FirstName && author.LastName == artist.LastName) {
                        displayArtist = false;
                    }
                });
            }

            if (displayArtist) {
                if (artist.FirstName && artist.LastName) {
                    artistContainer.innerHTML += `<span>${artist.FirstName} ${artist.LastName}</span>`;
                } else if (artist.FirstName) {
                    artistContainer.innerHTML += `<span>${artist.FirstName}</span>`;
                } else if (artist.LastName) {
                    artistContainer.innerHTML += `<span>${artist.LastName}</span>`;
                }
            }
        });
    }

    // Update the radio button selection
    const radioButtons = document.querySelectorAll('.radio-buttons-container input[type="radio"]');
    radioButtons.forEach((radio, i) => {
        radio.checked = (i === index);
    });
}

function switchBook(radioButton) {
    let newIndex = parseInt(radioButton.getAttribute('data-index'));
    if (newIndex < books.length) {
        index = newIndex;
        updateDetails(books[index]);  // ensure the radio button and image are synced

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
    // Set the initial book details
    updateDetails(books[0]);
}