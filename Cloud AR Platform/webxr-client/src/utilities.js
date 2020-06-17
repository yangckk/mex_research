const socket = io.connect('https://158c54bb0aa6.ngrok.io', {reconnect: true});

function convertToBase64(buffer) {
    return btoa(String.fromCharCode(...new Uint8Array(buffer)));
}

function log(message) {
    socket.emit('log', message);
}
function error(message) {
    socket.emit('error', message);
}
function sendPose(pose) {
    socket.emit('pose', pose);
}