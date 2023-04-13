function createPhotoInput(rowCount) {
    return '<label for="Photo_' + rowCount + '">Author ' + (rowCount + 1) + ' Photo</label>' +
        '<input type="file" id="Photo_' + rowCount + '" name="Author[' + rowCount + '].Photo" data-author-index="' + rowCount + '" class="form-control-file  custom-file custom-file-input" />';
}
// Create a function to generate a new associated name input
function createAssociatedNameInput(rowCount, index) {
    return '<label for="Author_' + rowCount + '__AssociatedNames_' + index + '_nameString">Associated Name ' + (index + 1) + '</label>' +
        '<input type="text" id="Author_' + rowCount + '__AssociatedNames_' + index + '_nameString" name="Author[' + rowCount + '].AssociatedNames[' + index + '].nameString" class="form-control" />';
}

// Create a function to generate a new official website input
function createOfficialWebsiteInput(rowCount, index) {
    return '<div class="official-websites-container">' +
        '<h3>Official Website ' + (index + 1) + '</h3>' +
        '<label for="Author_' + rowCount + '__OfficalWebsites_' + index + '_OfficalWebsiteName">Website Name</label>' +
        '<input type="text" id="Author_' + rowCount + '__OfficalWebsites_' + index + '_OfficalWebsiteName" name="Author[' + rowCount + '].OfficalWebsites[' + index + '].OfficalWebsiteName" class="form-control" />' +
        '<label for="Author_' + rowCount + '__OfficalWebsites_' + index + '_Twitter">Twitter</label>' +
        '<input type="text" id="Author_' + rowCount + '__OfficalWebsites_' + index + '_Twitter" name="Author[' + rowCount + '].OfficalWebsites[' + index + '].Twitter" class="form-control" />' +
        '<label for="Author_' + rowCount + '__OfficalWebsites_' + index + '_Facebook">Facebook</label>' +
        '<input type="text" id="Author_' + rowCount + '__OfficalWebsites_' + index + '_Facebook" name="Author[' + rowCount + '].OfficalWebsites[' + index + '].Facebook" class="form-control" />' +
        '<label for="Author_' + rowCount + '__OfficalWebsites_' + index + '_Line">Line</label>' +
        '<input type="text" id="Author_' + rowCount + '__OfficalWebsites_' + index + '_Line" name="Author[' + rowCount + '].OfficalWebsites[' + index + '].Line" class="form-control" />' +
        '<label for="Author_' + rowCount + '__OfficalWebsites_' + index + '_Naver">Naver</label>' +
        '<input type="text" id="Author_' + rowCount + '__OfficalWebsites_' + index + '_Naver" name="Author[' + rowCount + '].OfficalWebsites[' + index + '].Naver" class="form-control" />' +
        '<label for="Author_' + rowCount + '__OfficalWebsites_' + index + '_Instagram">Instagram</label>' +
        '<input type="text" id="Author_' + rowCount + '__OfficalWebsites_' + index + '_Instagram" name="Author[' + rowCount + '].OfficalWebsites[' + index + '].Instagram" class="form-control" />' +
        '<label for="Author_' + rowCount + '__OfficalWebsites_' + index + '_OfficalWebsiteString">Website URL</label>' +
        '<input type="text" id="Author_' + rowCount + '__OfficalWebsites_' + index + '_OfficalWebsiteString" name="Author[' + rowCount + '].OfficalWebsites[' + index + '].OfficalWebsiteString" class="form-control" />' +
        '</div>';
}

$("#addRow").click(function () {
    var rowCount = parseInt($("#total").val());
    console.log("Add button clicked");
    rowCount++;
    $("#total").val(rowCount);
    var html = '';
    html += '<div id="inputRow">';
    html += '<h3>Author </h3>';
    html += '<label for="Author_' + (rowCount - 1) + '__FirstName">Author ' + rowCount + ' First Name</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__FirstName" name="Author[' + (rowCount - 1) + '].FirstName" class="form-control" required />';
    html += '<label for="Author_' + (rowCount - 1) + '__LastName">Author ' + rowCount + ' Last Name</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__LastName" name="Author[' + (rowCount - 1) + '].LastName" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__PhotoPath">Author ' + rowCount + ' Photo </label>';
    html += '<input type="file" id="Photo_' + (rowCount - 1) + '" name="Photo" data-author-index="' + (rowCount - 1) + '" class="form-control-file custom-file custom-file-input" />';
    html += '<input type="hidden" id="HiddenPhoto_' + (rowCount - 1) + '" name="Photo[' + (rowCount - 1) + ']" value="/Images/AuthorImage/NoPhoto.png" />';

    $(document).on('change', 'input[type="file"].custom-file-input', function () {
        var file = this.files[0];
        if (file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                // File successfully read, you can process the file here
            };
            reader.readAsDataURL(file);

            // Replace the current hidden input field value with the new file value
            var authorIndex = $(this).data('author-index');
            $('#HiddenPhoto_' + authorIndex).val(file.name);
        } else {
            // File input is empty, use the default image path
            $(this).data('img-path', '/Images/AuthorImage/NoPhoto.png');
        }
    });
    html += '<br />'
    /*   html += createPhotoInput(rowCount - 1);*/
    html += '<br />'
    html += '<label for="Author_' + (rowCount - 1) + '__Gender">Author ' + rowCount + ' Gender</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__Gender" name="Author[' + (rowCount - 1) + '].Gender" class="form-control" />';

    html += '<label for="Author_' + (rowCount - 1) + '__Twitter">Author ' + rowCount + ' Twitter</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__Twitter" name="Author[' + (rowCount - 1) + '].Twitter" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__reddit">Author ' + rowCount + ' Reddit</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__reddit" name="Author[' + (rowCount - 1) + '].reddit" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__BirthPlace">Author ' + rowCount + ' Birth Place</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__BirthPlace" name="Author[' + (rowCount - 1) + '].BirthPlace" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__Biography">Author ' + rowCount + ' Biography</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__Biography" name="Author[' + (rowCount - 1) + '].Biography" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__NameInNative">Author ' + rowCount + ' Name In Native</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__NameInNative" name="Author[' + (rowCount - 1) + '].NameInNative" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__WikiPedia">Author ' + rowCount + ' Wikipedia</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__WikiPedia" name="Author[' + (rowCount - 1) + '].WikiPedia" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__AmountOfWork">Author ' + rowCount + ' Amount of Work</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__AmountOfWork" name="Author[' + (rowCount - 1) + '].AmountOfWork" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__WorkingAt">Author ' + rowCount + ' Working At</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__WorkingAt" name="Author[' + (rowCount - 1) + '].WorkingAt" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__LastUpdate">Author ' + rowCount + ' Last Update</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__LastUpdate" name="Author[' + (rowCount - 1) + '].LastUpdate" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__AuthorBorn">Author ' + rowCount + ' Born</label>';
    html += '<input type="date" id="Author_' + (rowCount - 1) + '__AuthorBorn" name="Author[' + (rowCount - 1) + '].AuthorBorn" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__AuthorDeath">Author ' + rowCount + ' Death</label>';
    html += '<input type="date" id="Author_' + (rowCount - 1) + '__AuthorDeath" name="Author[' + (rowCount - 1) + '].AuthorDeath" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__Contact">Author ' + rowCount + ' Contact</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__Contact" name="Author[' + (rowCount - 1) + '].Contact" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__MangaModels">Author ' + rowCount + ' Manga Models</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__MangaModels" name="Author[' + (rowCount - 1) + '].MangaModels" class="form-control" />';

    html += '<button id="removeRow" type="button" class="btn btn-danger">Remove</button>';
    html += '</div>';
    html += '</div>';

    // Add input fields for AssociatedNames

    html += '<div class="associated-names-container">';
    html += '<h3>Associated Names</h3>';
    html += '</div>';
    // Add an 'Add Associated Name' button for each author
    html += '<button type="button" class="btn btn-primary add-associated-name">Add Associated Name</button>';
    html += '<div class="official-websites-container">';

    html += '</div>';
    // Add input fields for OfficalWebsites

    // Add an 'Add Offical Website' button for each author
    html += '<button type="button" class="btn btn-primary add-official-website">Add Official Website</button>';

    $('#formRows').append(html);
});
// Add a new associated name input when the 'Add Associated Name' button is clicked
$(document).on('click', '.add-associated-name', function () {
    var rowCount = parseInt($("#total").val()) - 1;
    var associatedNameCount = $(this).siblings('.associated-names-container').find('input[type="text"][name*="AssociatedNames"]').length;
    $(this).siblings('.associated-names-container').append(createAssociatedNameInput(rowCount, associatedNameCount));
});

// Add a new official website input when the 'Add Official Website' button is clicked
$(document).on('click', '.add-official-website', function () {
    var rowCount = parseInt($("#total").val()) - 1;
    var officialWebsiteCount = $(this).siblings('.official-websites-container').find('input[type="text"][name*="OfficalWebsites"]').length / 7; // Divide by 7 as there are 7 input fields per official website
    var newOfficialWebsiteInput = createOfficialWebsiteInput(rowCount, officialWebsiteCount);
    $(this).siblings('.official-websites-container').append(newOfficialWebsiteInput);
});
$(document).on('click', '#removeRow', function () {
    var rowCount = parseInt($("#total").val());
    rowCount--;
    $("#total").val(rowCount);
    $(this).closest('#inputRow').remove();
});
      // $(document).on('change', 'input[type="file"][data-author-index]', function () {
    //    var file = this.files[0];
    //    var authorIndex = $(this).data('author-index');

    //    if (file) {
    //        // Remove the default photo path from the data attribute
    //        $(this).data('default-photo', '');
    //    } else {
    //        if (!$(this).data('default-photo')) {
    //            var defaultPhotoPath = '/Images/AuthorImage/NoPhoto.png';
    //            $(this).data('default-photo', defaultPhotoPath);
    //        }
    //        // Set the value of the file input to the default photo path
    //        $(this).val($(this).data('default-photo'));
    //    }
    //});