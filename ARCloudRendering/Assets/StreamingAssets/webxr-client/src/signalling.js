const url = "wss://10.0.0.218:5000";
const name = "Client";
const webSocket = new WebSocket(url);
let connected = false;

webSocket.onopen = function (event) {
    login();
}

function login() {
    webSocket.send(JSON.stringify({"type": "login", "name": name}));
}

function sendOffer(sdp) {
    webSocket.send(JSON.stringify({"type": "offer", "offer": sdp, "name": name}));
}