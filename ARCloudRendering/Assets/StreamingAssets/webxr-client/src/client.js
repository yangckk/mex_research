const reader = new FileReader();

let imageCanvas = null;
let context = null;

socket.onopen = function(event) {
    connected = true;
}

socket.onclose = function(event) {
    connected = false;
}

socket.onmessage = function(event) {
    imageCanvas = document.getElementById("image-canvas");
    context = imageCanvas.getContext('2d');
    new Response(event.data).arrayBuffer()
        .then(function(buffer) {
            const base64 = convertToBase64(buffer);
            return base64;
        })
        .then(function(base64) {
            try {
                let image = new Image();
                image.src = "data:image/png;base64," + base64;
                image.onload = function () {
                    context.clearRect(0, 0, imageCanvas.width, imageCanvas.height);
                    context.drawImage(image, 0, 0, imageCanvas.width, imageCanvas.height);
                }
            } catch (e) {
                error("Error displaying image");
            }
        })
        .catch(e => error(e));    
}