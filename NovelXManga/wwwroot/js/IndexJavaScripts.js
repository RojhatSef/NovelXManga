document.addEventListener("DOMContentLoaded", function () {
    const miniCards = document.querySelector(".MiniCards");
    const leftArrow = document.querySelector(".left-arrow");
    const rightArrow = document.querySelector(".right-arrow");
    const items = miniCards.querySelectorAll('.BoxOne');
    let firstItem = null;
    let lastItem = null;

    function loopRight() {
        firstItem = miniCards.querySelector('.BoxOne');
        miniCards.appendChild(firstItem);
    }

    function loopLeft() {
        lastItem = miniCards.querySelectorAll('.BoxOne')[items.length - 1];
        miniCards.insertBefore(lastItem, miniCards.firstChild);
    }

    rightArrow.addEventListener('click', function () {
        loopRight();
    });

    leftArrow.addEventListener('click', function () {
        loopLeft();
    });
});