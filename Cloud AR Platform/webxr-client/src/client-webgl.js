const transparency = true; //true if PNG, false if JPG

const canvas = document.getElementById("image-canvas");
const context = canvas.getContext('2d');
// const sandbox = new GlslCanvas(canvas);

const socket = io.connect('https://f30381367946.ngrok.io', {reconnect: true});
const reader = new FileReader();
socket.on('connect', function (s) {
    console.log('Connected');
});
socket.on('image', function(data) {

    if (transparency) {
        const base64 = convertToBase64(data);
        console.log(base64);
        // const base64 = new Buffer(data, 'base64');
        let image = new Image();
        image.src = "data:image/png;base64," + base64;
        image.onload = function () {
            context.clearRect(0, 0, canvas.width, canvas.height);
            context.drawImage(image, 0, 0, canvas.width, canvas.height);
        }
    } else {
        sandbox.setUniform('u_encodedImage', data);
    }
});