(function () {
    let containers = document.querySelectorAll(".rating-container");
    containers.forEach(container => {
        let stars = container.querySelectorAll("input");
        let category = container.getAttribute("data-category");
        let showValue = document.querySelector("#rating-" + category);
        stars.forEach(star => {
            star.addEventListener("click", function () {
                let value = this.value;
                showValue.textContent = value + " Out of 5";

                // If you need to send this data to the backend immediately, you can do so here
                // sendToBackend(category, value);
            });
        });
    });
})();