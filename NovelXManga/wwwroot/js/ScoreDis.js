var pageNumber = 1;
$(document).ready(function () {
    // Hide the bar values initially
    console.log("JavaScript execution started");
    pageNumber = pageNumber + 1;
    $('.bar span').hide();

    // Calculate the maximum count for scaling the bars
    var maxCount = 0;
    $('.bar').each(function () {
        var count = parseInt($(this).text());
        if (count > maxCount) {
            maxCount = count;
        }
    });

    // Animate the bars based on their count
    $('.bar').each(function () {
        var count = parseInt($(this).text());
        var percentage = (count / maxCount) * 100;
        $(this).css('width', '0%').animate({
            width: percentage + '%'
        }, 1000);
    });

    // Fade in the bar values after the animation
    setTimeout(function () {
        $('.bar span').fadeIn('slow');
    }, 1000);
});