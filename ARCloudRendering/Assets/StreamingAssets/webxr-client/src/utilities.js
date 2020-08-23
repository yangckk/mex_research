const socket = new WebSocket('wss://0a9175c2e865.ngrok.io/AR');
let connected = false;
function convertToBase64(buffer) {
    return btoa(String.fromCharCode.apply(null, new Uint8Array(buffer)));
}

function log(message) {
    if (connected)
        socket.send(JSON.stringify({'log': message}));
}
function error(message) {
    if (connected)
        socket.send(JSON.stringify({'error': message}));
}
function sendPose(pose) {
    if (connected)
        socket.send(pose);
}