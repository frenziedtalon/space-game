"use strict";
class NavigationCamera {
    constructor(target: BABYLON.Mesh, scene: BABYLON.Scene, cameraHelper: CameraHelper) {
        const camera = new BABYLON.TargetCamera(target.name + cameraHelper.NavCameraNameEnd, BABYLON.Vector3.Zero(), scene);
        camera.setTarget(target.position);
        camera.layerMask = 2; // 010 in binary

        // tag the target with this camera's id, for later retrieval
        BABYLON.Tags.AddTagsTo(target, camera.id);

        scene.activeCameras.push(camera);
    }
}