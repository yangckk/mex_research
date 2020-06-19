//const socket = io.connect('https://158c54bb0aa6.ngrok.io', {reconnect: true});
const socket = new WebSocket('wss://10.0.0.218:443/AR');
let connected = false;
function convertToBase64(buffer) {
    return btoa(String.fromCharCode(...new Uint8Array(buffer)));
}

function log(message) {
    //socket.emit('log', message);
}
function error(message) {
    //socket.emit('error', message);
}
function sendPose(pose) {
    if (connected)
        socket.send(pose);
}