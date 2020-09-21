let xrSession = null;
let xrRefSpace = null;
let poseTransform = null;

// WebGL scene globals.
let gl = null;
let canvas = null;

function initXR() {
    if (!xrSession) {
        navigator.xr.requestSession('immersive-ar', {
            optionalFeatures: ['dom-overlay'],
            domOverlay: {root: document.getElementById('overlay')}
        }).then(onSessionStarted);
        log("AR Session Started");
    } else {
        xrSession.end();
    }
}

function onSessionStarted(session) {
    xrSession = session;

    session.addEventListener('end', onSessionEnded);
    canvas = document.createElement('canvas');
    gl = canvas.getContext('webgl', {
        xrCompatible: true
    });
    session.updateRenderState({ baseLayer: new XRWebGLLayer(session, gl) });

    session.requestReferenceSpace('local').then((refSpace) => {
        xrRefSpace = refSpace;
        session.requestAnimationFrame(onXRFrame);
    });
}

function onRequestSessionError(ex) {
    alert("Failed to start immersive AR session.");
    error(ex.message);
}

function onEndSession(session) {
    session.end();
}

function onSessionEnded(event) {
    xrSession = null;
    gl = null;
}

function onXRFrame(t, frame) {
    let session = frame.session;
    session.requestAnimationFrame(onXRFrame);
    let pose = frame.getViewerPose(xrRefSpace);

    if (pose) {
        poseTransform = pose.transform.inverse;
        sendPose(JSON.stringify({position: poseTransform.position, rotation: poseTransform.orientation}));
    }
}

initXR();