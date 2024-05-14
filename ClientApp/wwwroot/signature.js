console.log("signature.js has been loaded");

let signaturePad;

function initializeSignaturePad(canvasId) {
    console.log("Attempting to initialize SignaturePad on canvas with ID:", canvasId);

    if (typeof SignaturePad === 'undefined') {
        console.error("SignaturePad is not defined. Ensure that signature_pad.min.js is loaded properly.");
        return;
    }

    const canvas = document.getElementById(canvasId);
    if (canvas) {
        signaturePad = new SignaturePad(canvas);
        console.log("SignaturePad initialized:", signaturePad);
    } else {
        console.error("Canvas not found:", canvasId);
    }
}

function clearSignaturePad() {
    if (signaturePad) {
        signaturePad.clear();
        console.log("SignaturePad cleared");
    } else {
        console.error("SignaturePad not initialized");
    }
}

function getSignatureDataUrl() {
    if (signaturePad && !signaturePad.isEmpty()) {
        return signaturePad.toDataURL();
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
