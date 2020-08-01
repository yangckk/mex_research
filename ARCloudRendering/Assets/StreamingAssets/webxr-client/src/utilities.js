const socket = new WebSocket('wss://487a022a4407.ngrok.io/signalling');
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
function sendToSignallingServer(data) {
    if (connected)
        socket.send(JSON.stringify(data));
}