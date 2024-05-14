console.log("site.js has been loaded");

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev, targetId) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    var draggedElement = document.getElementById(data);
    var targetElement = document.getElementById(targetId);
    if (targetElement && draggedElement) {
        targetElement.appendChild(draggedElement); // Optional visual feedback
    }
    DotNet.invokeMethodAsync('ClientApp', 'UpdateApplicationPeriod', data, targetId);
}

