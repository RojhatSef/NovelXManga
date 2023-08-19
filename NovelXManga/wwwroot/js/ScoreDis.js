$(document).ready(function () {
    $('.bar span').hide();

    var maxReviews = 0;
    for (var score in scoreDistribution) {
        if (scoreDistribution[score] > maxReviews) {
            maxReviews = scoreDistribution[score];
        }
    }

    for (var score in scoreDistribution) {
        var percentage = (scoreDistribution[score] / maxReviews) * 100;
        var barId = '#bar-';
        switch (parseInt(score)) {
            case 5: barId += 'five'; break;
            case 4: barId += 'four'; break;
            case 3: barId += 'three'; break;
            case 2: barId += 'two'; break;
            case 1: barId += 'one'; break;
        }

        $(barId).animate({
            width: percentage + '%'
        }, 1000);
    }

    setTimeout(function () {
        $('.bar span').fadeIn('slow');
    }, 1000);
});