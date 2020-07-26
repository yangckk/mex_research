const url = "https://bf6fb29fd9d6.ngrok.io";
const webSocket = new WebSocket(url);

localConnection = new RTCPeerConnection();
sendChannel = localConnection.createDataChannel("sendChannel");
sendChannel.onopen = ev => {
    console.log(ev);
};
let receiveChannel = null;
localConnection.ondatachannel = ev => {
    receiveChannel = ev.channel;
};
localConnection.onicecandidate = e => {
    if (e.candidate)
        sendToServer({
            type: 'offer',
            data: e.candidate
        });
};

localConnection.createOffer()
    .then(offer => localConnection.setLocalDescription(offer))
    .then(() => sendToServer({
        type: 'offer',
        data: localConnection.localDescription
    }))
    .catch(error => console.error(error));

webSocket.onmessage = function (event) {
    console.log(event.data);
    console.log(event.data.type);
};

function sendToServer(json) {
    var msg = JSON.stringify(json);
    return webSocket.send(msg);
}