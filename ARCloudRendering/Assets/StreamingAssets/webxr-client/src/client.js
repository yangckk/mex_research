let playButton = document.getElementById("play-button");
let overlay = document.getElementById("overlay");
let videoElement = document.getElementById("ar-video");

let sandbox = new GlslCanvas(arCanvas);

let candidates = []

localConnection = new RTCPeerConnection({
    iceServers: [     
      {
        urls: "stun:stun.l.google.com:19302"
      }
    ]
});
sendChannel = localConnection.createDataChannel("pose");

let receiveChannel = null;
localConnection.ondatachannel = ev => {
    receiveChannel = ev.channel;
};

localConnection.oniceconnectionstatechange = function(){
    console.log('ICE state: ', localConnection.iceConnectionState);
}

localConnection.ontrack = ev => {
    console.log("Video track received");
    videoElement.srcObject = ev.streams[0];
    videoElement.play();
}

localConnection.onicecandidate = e => {
    if (e.candidate)
        sendToSignallingServer({type: "candidate", candidate: e.candidate});
};

playButton.addEventListener('click', function(event) {
    localConnection.createOffer({offerToReceiveVideo: true})
    .then(offer => localConnection.setLocalDescription(offer))
    .then(() => sendToSignallingServer(localConnection.localDescription))
    .catch(error => console.error(error));

    console.log("Offer created");
    document.body.removeChild(playButton);
});

socket.onopen = function(event) {
    connected = true;
}

socket.onclose = function(event) {
    connected = false;
}

socket.onmessage = function (event) {
    const body = JSON.parse(event.data);

    switch (body.type) {
        case "answer":
            console.log("Received answer");
            const desc = new RTCSessionDescription(body);
            localConnection.setRemoteDescription(desc);
            candidates.forEach(candidate => localConnection.addIceCandidate(candidate));
            break;
        case "candidate":
            console.log("Received ICE candidate");
            const candidate = new RTCIceCandidate(body.candidate);
            candidates.push(candidate);
            break;
    }
};