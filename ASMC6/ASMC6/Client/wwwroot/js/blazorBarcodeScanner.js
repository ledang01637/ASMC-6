function generateQrCode(text) {
    const qrcode = document.getElementById("qrcode");

    new QRCode(qrcode, {
        text: text,
        width: 128,
        height: 128,
    });
}

function clearQrCode() {
    document.getElementById("qrcode").innerHTML = "";
}

