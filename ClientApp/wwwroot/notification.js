window.showNotification = (message) => {
    var notification = document.getElementById("notification");
    notification.textContent = message || "Ansøgning indsendt med succes!";
    notification.classList.add("show");
    setTimeout(function () {
        notification.classList.remove("show");
    }, 5000); // Skjuler notifikationen efter 3 sekunder
};