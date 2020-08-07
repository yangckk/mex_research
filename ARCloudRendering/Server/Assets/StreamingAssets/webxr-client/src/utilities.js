const socket = new WebSocket('wss://c6070ff333e7.ngrok.io/AR');
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