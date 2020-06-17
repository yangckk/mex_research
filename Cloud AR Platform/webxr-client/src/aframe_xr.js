AFRAME.registerComponent('track-rotation', {
    tick: (function () {
        let pos = new THREE.Vector3();
        let quaternion = new THREE.Quaternion();

        return function () {
            this.el.object3D.getWorldPosition(pos);
            this.el.object3D.getWorldQuaternion(quaternion);
            sendPose({position: pos, rotation: quaternion});
        };
    })
});