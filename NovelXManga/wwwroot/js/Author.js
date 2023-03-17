$("#addRow").click(function () {
    var rowCount = parseInt($("#total").val());
    console.log("Add button clicked");
    rowCount++;
    $("#total").val(rowCount);
    var html = '';
    html += '<div id="inputRow">';
    html += '<label for="Author_' + (rowCount - 1) + '__FirstName">Author ' + rowCount + ' First Name</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__FirstName" name="Author[' + (rowCount - 1) + '].FirstName" class="form-control" required />';
    html += '<label for="Author_' + (rowCount - 1) + '__LastName">Author ' + rowCount + ' Last Name</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__LastName" name="Author[' + (rowCount - 1) + '].LastName" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__PhotoPath">Author ' + rowCount + ' Photo Path</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__PhotoPath" name="Author[' + (rowCount - 1) + '].PhotoPath" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__Gender">Author ' + rowCount + ' Gender</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__Gender" name="Author[' + (rowCount - 1) + '].Gender" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__OfficalWebsites">Author ' + rowCount + ' Official Websites</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__OfficalWebsites" name="Author[' + (rowCount - 1) + '].OfficalWebsites" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__Twitter">Author ' + rowCount + ' Twitter</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__Twitter" name="Author[' + (rowCount - 1) + '].Twitter" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__reddit">Author ' + rowCount + ' Reddit</label>';
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
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__AuthorBorn" name="Author[' + (rowCount - 1) + '].AuthorBorn" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__AuthorDeath">Author ' + rowCount + ' Death</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__AuthorDeath" name="Author[' + (rowCount - 1) + '].AuthorDeath" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__Contact">Author ' + rowCount + ' Contact</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__Contact" name="Author[' + (rowCount - 1) + '].Contact" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__MangaModels">Author ' + rowCount + ' Manga Models</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__MangaModels" name="Author[' + (rowCount - 1) + '].MangaModels" class="form-control" />';
    html += '<label for="Author_' + (rowCount - 1) + '__isChecked">Author ' + rowCount + ' is Checked</label>';
    html += '<input type="checkbox" id="Author_' + (rowCount - 1) + '__isChecked" name="Author[' + (rowCount - 1) + '].isChecked" />';
    html += '<label for="Author_' + (rowCount - 1) + '__AssociatedNames">Author ' + rowCount + ' Associated Names</label>';
    html += '<input type="text" id="Author_' + (rowCount - 1) + '__AssociatedNames" name="Author[' + (rowCount - 1) + '].AssociatedNames" class="form-control" multiple />';
    html += '<button id="removeRow" type="button" class="btn btn-danger">Remove</button>';
    html += '</div>';
    //add more inputs here...

    $('#newRow').append(html);
});
$(document).on('click', '#removeRow', function () {
    var rowCount = parseInt($("#total").val());
    rowCount--;
    $("#total").val(rowCount);
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