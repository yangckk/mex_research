const reader = new FileReader();

let imageCanvas = null;
let context = null;

socket.on('connect', function (s) {

});

socket.on('image', function(data) {
    imageCanvas = document.getElementById("image-canvas");
    context = imageCanvas.getContext('2d');
    const base64 = convertToBase64(data);

    try {
        let image = new Image();
        image.src = "data:image/png;base64," + base64;
        image.onload = function () {
            context.clearRect(0, 0, imageCanvas.width, imageCanvas.height);
            context.drawImage(image, 0, 0, imageCanvas.width, imageCanvas.height);

            // log("Image displayed");
        }
    } catch (e) {
        log("Error displaying image");
    }
});