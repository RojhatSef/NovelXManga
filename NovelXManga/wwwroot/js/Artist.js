// Create a function to generate a new associated name input
function createAssociatedNameInput(rowCount, index) {
    return '<label for="Artist_' + rowCount + '__AssociatedNames_' + index + '_nameString">Associated Name ' + (index + 1) + '</label>' +
        '<input type="text" id="Artist_' + rowCount + '__AssociatedNames_' + index + '_nameString" name="Artist[' + rowCount + '].AssociatedNames[' + index + '].nameString" class="form-control" />';
}

// Create a function to generate a new official website input
function createOfficialWebsiteInput(rowCount, index) {
    return '<div class="official-websites-container-artist">' +
        '<h3>Official Website ' + (index + 1) + '</h3>' +
        '<label for="Artist_' + rowCount + '__OfficalWebsites_' + index + '_OfficalWebsiteName">Website Name</label>' +
        '<input type="text" id="Artist_' + rowCount + '__OfficalWebsites_' + index + '_OfficalWebsiteName" name="Artist[' + rowCount + '].OfficalWebsites[' + index + '].OfficalWebsiteName" class="form-control" />' +
        '<label for="Artist_' + rowCount + '__OfficalWebsites_' + index + '_Twitter">Twitter</label>' +
        '<input type="text" id="Artist_' + rowCount + '__OfficalWebsites_' + index + '_Twitter" name="Artist[' + rowCount + '].OfficalWebsites[' + index + '].Twitter" class="form-control" />' +
        '<label for="Artist_' + rowCount + '__OfficalWebsites_' + index + '_Facebook">Facebook</label>' +
        '<input type="text" id="Artist_' + rowCount + '__OfficalWebsites_' + index + '_Facebook" name="Artist[' + rowCount + '].OfficalWebsites[' + index + '].Facebook" class="form-control" />' +
        '<label for="Artist_' + rowCount + '__OfficalWebsites_' + index + '_Line">Line</label>' +
        '<input type="text" id="Artist_' + rowCount + '__OfficalWebsites_' + index + '_Line" name="Artist[' + rowCount + '].OfficalWebsites[' + index + '].Line" class="form-control" />' +
        '<label for="Artist_' + rowCount + '__OfficalWebsites_' + index + '_Naver">Naver</label>' +
        '<input type="text" id="Artist_' + rowCount + '__OfficalWebsites_' + index + '_Naver" name="Artist[' + rowCount + '].OfficalWebsites[' + index + '].Naver" class="form-control" />' +
        '<label for="Artist_' + rowCount + '__OfficalWebsites_' + index + '_Instagram">Instagram</label>' +
        '<input type="text" id="Artist_' + rowCount + '__OfficalWebsites_' + index + '_Instagram" name="Artist[' + rowCount + '].OfficalWebsites[' + index + '].Instagram" class="form-control" />' +
        '<label for="Artist_' + rowCount + '__OfficalWebsites_' + index + '_OfficalWebsiteString">Website URL</label>' +
        '<input type="text" id="Artist_' + rowCount + '__OfficalWebsites_' + index + '_OfficalWebsiteString" name="Artist[' + rowCount + '].OfficalWebsites[' + index + '].OfficalWebsiteString" class="form-control" />' +
        '</div>';
}
$("#addRowArtist").click(function () {
    var rowCount = parseInt($("#totalArtist").val());
    console.log("Add button clicked");
    rowCount++;
    $("#totalArtist").val(rowCount);
    var html = '';
    html += '<div id="inputRow">';
    html += '<h3>Artist</h3>';
    html += '<label for="Artist_' + (rowCount - 1) + '__FirstName">Artist ' + rowCount + ' First Name</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__FirstName" name="Artist[' + (rowCount - 1) + '].FirstName" class="form-control" required />';
    html += '<label for="Artist_' + (rowCount - 1) + '__LastName">Artist ' + rowCount + ' Last Name</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__LastName" name="Artist[' + (rowCount - 1) + '].LastName" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__PhotoPath">Artist ' + rowCount + ' Photo Path</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__PhotoPath" name="Artist[' + (rowCount - 1) + '].PhotoPath" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__Gender">Artist ' + rowCount + ' Gender</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__Gender" name="Artist[' + (rowCount - 1) + '].Gender" class="form-control" />';

    html += '<label for="Artist_' + (rowCount - 1) + '__Twitter">Artist ' + rowCount + ' Twitter</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__Twitter" name="Artist[' + (rowCount - 1) + '].Twitter" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__reddit">Artist ' + rowCount + ' Reddit</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__reddit" name="Artist[' + (rowCount - 1) + '].reddit" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__BirthPlace">Artist ' + rowCount + ' Birth Place</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__BirthPlace" name="Artist[' + (rowCount - 1) + '].BirthPlace" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__Biography">Artist ' + rowCount + ' Biography</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__Biography" name="Artist[' + (rowCount - 1) + '].Biography" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__NameInNative">Artist ' + rowCount + ' Name In Native</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__NameInNative" name="Artist[' + (rowCount - 1) + '].NameInNative" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__WikiPedia">Artist ' + rowCount + ' Wikipedia</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__WikiPedia" name="Artist[' + (rowCount - 1) + '].WikiPedia" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__AmountOfWork">Artist ' + rowCount + ' Amount of Work</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__AmountOfWork" name="Artist[' + (rowCount - 1) + '].AmountOfWork" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__WorkingAt">Artist ' + rowCount + ' Working At</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__WorkingAt" name="Artist[' + (rowCount - 1) + '].WorkingAt" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__LastUpdate">Artist ' + rowCount + ' Last Update</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__LastUpdate" name="Artist[' + (rowCount - 1) + '].LastUpdate" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__ArtistBorn">Artist ' + rowCount + ' Born</label>';
    html += '<input type="date" id="Artist_' + (rowCount - 1) + '__ArtistBorn" name="Artist[' + (rowCount - 1) + '].ArtistBorn" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__ArtistDeath">Artist ' + rowCount + ' Death</label>';
    html += '<input type="date" id="Artist_' + (rowCount - 1) + '__ArtistDeath" name="Artist[' + (rowCount - 1) + '].ArtistDeath" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__Contact">Artist ' + rowCount + ' Contact</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__Contact" name="Artist[' + (rowCount - 1) + '].Contact" class="form-control" />';
    html += '<label for="Artist_' + (rowCount - 1) + '__MangaModels">Artist ' + rowCount + ' Manga Models</label>';
    html += '<input type="text" id="Artist_' + (rowCount - 1) + '__MangaModels" name="Artist[' + (rowCount - 1) + '].MangaModels" class="form-control" />';

    html += '<button id="removeRow" type="button" class="btn btn-danger">Remove</button>';
    html += '</div>';
    html += '</div>';
    //add more inputs here...

    html += '<div class="associated-names-container-artist">';
    html += '<h3>Associated Names</h3>';
    html += '</div>';

    // Add an 'Add Associated Name' button for each Artist
    html += '<button type="button" class="btn btn-primary add-associated-name-artist">Add Associated Name</button>';

    // Add input fields for OfficalWebsites
    html += '<div class="official-websites-container-artist">';
    html += '<h3>Official Websites</h3>';
    html += '</div>';

    // Add an 'Add Offical Website' button for each Artist
    html += '<button type="button" class="btn btn-primary add-official-website">Add Official Website</button>';

    $('#newRowArtist').append(html);
});
$(document).on('click', '.add-associated-name-artist', function () {
    var rowCount = parseInt($("#totalArtist").val()) - 1;
    var associatedNameCount = $(this).siblings('.associated-names-container-artist').find('input[type="text"][name*="AssociatedNames"]').length;
    $(this).siblings('.associated-names-container-artist').append(createAssociatedNameInput(rowCount, associatedNameCount));
});

// Add a new official website input when the 'Add Official Website' button is clicked
$(document).on('click', '.add-official-website', function () {
    var rowCount = parseInt($("#totalArtist").val()) - 1;
    var officialWebsiteCount = $(this).siblings('.official-websites-container-artist').find('input[type="text"][name*="OfficalWebsites"]').length / 7; // Divide by 7 as there are 7 input fields per official website
    var newOfficialWebsiteInput = createOfficialWebsiteInput(rowCount, officialWebsiteCount);
    $(this).siblings('.official-websites-container-artist').append(newOfficialWebsiteInput);
});
$(document).on('click', '#removeRow', function () {
    var rowCount = parseInt($("#totalArtist").val());
    rowCount--;
    $("#totalArtist").val(rowCount);
    $(this).closest('#inputRow').remove();
});

//Orignal
//$("#addRow").click(function () {
//    var rowCount = parseInt($("#total").val());
//    console.log("Add button clicked");
//    rowCount++;
//    $("#total").val(rowCount);
//    var html = '';
//    html += '<div id="inputRow">';
//    html += '<input type="text" name="[' + (rowCount - 1) + '].Name"  />';
//    html += '<input type="number" name="[' + (rowCount - 1) + '].Age"  />';
//    //add more inputs here...
//    html += '<button id="removeRow" type="button" class="btn btn-danger">Remove</button>';
//    html += '</div>';

//    $('#newRow').append(html);
//});
//$(document).on('click', '#removeRow', function () {
//    var rowCount = parseInt($("#total").val());
//    rowCount--;
//    $("#total").val(rowCount);
//    $(this).closest('#inputRow').remove();
//});  