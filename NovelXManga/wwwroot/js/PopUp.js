function showPopup(message) {
    const popupContent = document.querySelector("#popup .popup-content");
    popupContent.innerHTML = `<p>${message}</p><button onclick="closePopup()">Close</button>`;
    document.querySelector("#popup").style.display = "block";
}

function closePopup() {
    document.querySelector("#popup").style.display = "none";
}