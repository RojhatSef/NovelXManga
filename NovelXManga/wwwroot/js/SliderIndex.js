﻿$(document).ready(function () {
    $('.slider').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        fade: true,
        prevArrow: $('.IndexArrowsPrev'),
        nextArrow: $('.IndexArrowsNext')
    });
});