function selectallchange(args) {
    var checkList = document.getElementById("checkList").ej2_instances[0];
    // enable or disable the SelectAll in multiselect on CheckBox checked state
    checkList.showSelectAll = args.checked;
}

function dropiconchange(args) {
    var checkList = document.getElementById("checkList").ej2_instances[0];
    // enable or disable the SelectAll in multiselect on CheckBox checked state
    checkList.showDropDownIcon = args.checked;
}
function reorderchange(args) {
    var checkList = document.getElementById("checkList").ej2_instances[0];
    // enable or disable the SelectAll in multiselect on CheckBox checked state
    checkList.enableSelectionOrder = args.checked;

    // hide or open tags

    $(document).ready(function () {
        $('#toggleTags').click(function () {
            $('#HideTagsWhen').toggleClass('hidden-tags');
        });
    });
}