document.addEventListener("DOMContentLoaded", function () {
    var pageNumber = 1;
    var pageSize = 8;
    var moreUpdatesButton = document.querySelector("#loadMoreUpdates");

    moreUpdatesButton.addEventListener("click", function (e) {
        e.preventDefault();

        pageNumber++;
        fetch(`/Manga/MoreUpdates?handler=LoadMore&pageNumber=${pageNumber}&pageSize=${pageSize}`)
            .then(response => response.json())
            .then(data => {
                var bookContainer = document.querySelector('.MUS-cardFourOfaKind');

                data.forEach(book => {
                    let bookDiv = document.createElement('div');
                    bookDiv.className = "MUS-FourOfAKind";

                    let anchor = document.createElement('a');
                    anchor.className = "MUS-ANoneStyle";
                    anchor.href = `/Manga/CurrentManga?id=${book.MangaID}`;

                    let img = document.createElement('img');
                    img.className = "MUS-BoxImage";
                    img.src = `~/Images/GeneratedMangaImage/${book.PhotoPath}`;

                    let contentDiv = document.createElement('div');
                    contentDiv.className = "MUS-FourofKindContent";

                    let h2 = document.createElement('h2');
                    h2.textContent = book.MangaName;

                    let p = document.createElement('p');
                    p.innerText = book.Description;

                    let tagContainer = document.createElement('div');
                    tagContainer.className = "MUS-tag-container MUS-DisplayWhenHoverFourOfaKind";

                    if (book.TagsModels) {
                        book.TagsModels.slice(0, 5).forEach(tag => {
                            let tagSpan = document.createElement('span');
                            tagSpan.className = "MUS-tag";
                            tagSpan.innerText = tag.TagName;
                            tagContainer.appendChild(tagSpan);
                        });
                    }

                    if (book.GenresModels) {
                        book.GenresModels.slice(0, 5).forEach(genre => {
                            let genreSpan = document.createElement('span');
                            genreSpan.className = "MUS-tag";
                            genreSpan.innerText = genre.GenreName;
                            tagContainer.appendChild(genreSpan);
                        });
                    }

                    let scoreDiv = document.createElement('div');
                    scoreDiv.innerText = `Score: ${book.Score}`;  // assuming book.Score is a property

                    contentDiv.appendChild(h2);
                    contentDiv.appendChild(p);
                    contentDiv.appendChild(span);
                    contentDiv.appendChild(scoreDiv);

                    anchor.appendChild(img);
                    anchor.appendChild(contentDiv);
                    bookDiv.appendChild(anchor);

                    bookContainer.appendChild(bookDiv);
                });
            });
    });
});