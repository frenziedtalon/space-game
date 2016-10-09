"use strict";
class NavigationCamera {
    constructor(target, scene, cameraHelper) {
        const camera = new BABYLON.TargetCamera(target.name + cameraHelper.NavCameraNameEnd, BABYLON.Vector3.Zero(), scene);
        camera.setTarget(target.position);
        camera.parent = target;
        camera.layerMask = 2; // 010 in binary
        scene.activeCameras.push(camera);
    }
}
