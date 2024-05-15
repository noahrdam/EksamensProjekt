console.log("signature.js has been loaded");

let signaturePad;
let youthSignaturePad;

function initializeSignaturePad(canvasId) {
    console.log("Attempting to initialize SignaturePad on canvas with ID:", canvasId);

    if (typeof SignaturePad === 'undefined') {
        console.error("SignaturePad is not defined. Ensure that signature_pad.min.js is loaded properly.");
        return;
    }

    const canvas = document.getElementById(canvasId);
    if (canvas) {
        if (canvasId === "signature-pad") {
            signaturePad = new SignaturePad(canvas);
            console.log("SignaturePad initialized:", signaturePad);
        } else if (canvasId === "youth-signature-pad") {
            youthSignaturePad = new SignaturePad(canvas);
            console.log("Youth SignaturePad initialized:", youthSignaturePad);
        }
    } else {
        console.error("Canvas not found:", canvasId);
    }
}

function clearSignaturePad(canvasId) {
    if (canvasId === "signature-pad" && signaturePad) {
        signaturePad.clear();
        console.log("SignaturePad cleared");
    } else if (canvasId === "youth-signature-pad" && youthSignaturePad) {
        youthSignaturePad.clear();
        console.log("Youth SignaturePad cleared");
    } else {
        console.error("SignaturePad not initialized");
    }
}

function getSignatureDataUrl(canvasId) {
    if (canvasId === "signature-pad" && signaturePad && !signaturePad.isEmpty()) {
        return signaturePad.toDataURL();
    } else if (canvasId === "youth-signature-pad" && youthSignaturePad && !youthSignaturePad.isEmpty()) {
        return youthSignaturePad.toDataURL();
    }
    return null;
}

document.addEventListener('DOMContentLoaded', (event) => {
    console.log('DOM fully loaded and parsed');
    if (typeof SignaturePad !== 'undefined') {
        console.log("SignaturePad is available after DOMContentLoaded.");
    } else {
        console.error("SignaturePad is not defined after DOMContentLoaded.");
    }
});