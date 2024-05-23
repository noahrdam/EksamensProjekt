window.showNotification = (message) => {
    var notification = document.getElementById("notification");
    notification.classList.add("show");
    setTimeout(function () {
        notification.classList.remove("show");
    }, 5000); // Skjuler notifikationen efter 3 sekunder
};