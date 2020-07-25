let peerConnection = new RTCPeerConnection();
let sendChannel = peerConnection.createDataChannel("pose");



peerConnection.onicecandidate = e => !e.candidate //send ice candidate

peerConnection.createOffer()
.then(offer => peerConnection.setLocalDescription(offer))
//.then() //send offer to server
//receive answer