document.addEventListener("DOMContentLoaded", function () {
    const releaseYearInput = document.getElementById('releaseYear');
    const endingYearInput = document.getElementById('endingYear');

    // Validate on form submission
    document.querySelector('form').addEventListener('submit', function (event) {
        const releaseYear = releaseYearInput.value;
        const endingYear = endingYearInput.value;

        if (releaseYear && endingYear && endingYear < releaseYear) {
            alert("Ending year cannot be earlier than the release year.");
            event.preventDefault(); // Prevent form submission
        }
    });
});