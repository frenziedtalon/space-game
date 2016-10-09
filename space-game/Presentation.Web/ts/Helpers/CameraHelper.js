"use strict";
const MeshHelper_1 = require("./MeshHelper");
const VectorHelper_1 = require("./VectorHelper");
class CameraHelper {
    constructor(engine) {
        this.engine = engine;
        this.MainSceneCameraName = "mainSceneCamera";
        this.NavCameraNameEnd = "NavCamera";
        this.navBarWidth = 0.6;
        this.y = 0;
        this.height = 0.2;
        this.canvas = engine.getRenderingCanvas();
        this.meshHelper = new MeshHelper_1.MeshHelper(this);
    }
    updateNavigationCameras(cameras) {
        const navCameraWidth = 0.2 * this.navBarWidth * 0.6;
        let navCamCount = 0;
        for (let i = 0; i < cameras.length; i++) {
            if (cameras[i].name.endsWith(this.NavCameraNameEnd)) {
                const c = cameras[i];
                const parent = c.parent;
                const x = navCamCount * navCameraWidth;
                c.viewport = new BABYLON.Viewport(x, this.y, navCameraWidth, this.height);
                c.position = this.calculateNavigationCameraPosition(parent);
                c.setTarget(parent.position);
                navCamCount += 1;
            }
        }
    }
    calculateNavigationCameraPosition(target) {
        const radius = -this.meshHelper.getMeshBoundingSphereRadius(target) * 4;
        let pointA = BABYLON.Vector3.Zero();
        const targetParent = target.parent;
        if (targetParent) {
            pointA = targetParent.position;
        }
        const unitVector = VectorHelper_1.VectorHelper.calculateUnitVector(pointA, target.position);
        return new BABYLON.Vector3(radius * unitVector.x, radius * unitVector.y, radius * unitVector.z);
    }
    // Moves the main scene camera to be last in the activeCameras list which is the only way I've found so far to make the god rays work
    setMainSceneCameraActive(cameras) {
        for (let i = 0; i < cameras.length; i++) {
            if (cameras[i].name === this.MainSceneCameraName) {
                const c = cameras[i];
                cameras.splice(i, 1);
                cameras.push(c);
                return;
            }
        }
    }
    convertCanvasToViewportPoint(clientPoint, hardwareScalingLevel) {
        // canvas - (0,0) is top left
        // viewport - (0,0) is bottom left
        // invert y-axis for picked position
        const y = this.canvas.height - (clientPoint.y / hardwareScalingLevel);
        const x = clientPoint.x / hardwareScalingLevel;
        return new BABYLON.Vector2(x, y);
    }
    getViewportCamera(clientPoint, cameras) {
        const viewportPoint = this.convertCanvasToViewportPoint(clientPoint, this.engine.getHardwareScalingLevel());
        // loop through cameras to find which one we clicked in. Go backwards as the main camera will be last in the active list and is the largest in area.
        for (let j = cameras.length - 1; j >= 0; j--) {
            const c = cameras[j];
            if (this.clickedInViewPort(viewportPoint, c)) {
                return c;
            }
        }
        return null;
    }
    clickedInViewPort(viewportPoint, camera) {
        const vp = camera.viewport;
        if ((viewportPoint.x > vp.x * this.canvas.width) && (viewportPoint.x < (vp.x + vp.width) * this.canvas.width)) {
            if ((viewportPoint.y > vp.y * this.canvas.height) && (viewportPoint.y < (vp.y + vp.height) * this.canvas.height)) {
                return true;
            }
        }
        return false;
    }
    setCameraTargetFromId(id, scene) {
        const mesh = scene.getMeshByID(id);
        this.setCameraTarget(mesh, scene);
    }
    setCameraTarget(mesh, scene) {
        if (!(mesh === null)) {
            let limit = this.meshHelper.getMeshBoundingSphereRadius(mesh) * 1.5;
            if (limit < 1) {
                limit = 1;
            }
        }
    }
    updateCameraTarget(targetId) {
        const data = "target=" + targetId;
        $.ajax({
            url: "../SpaceGameApi/api/Camera/SetTarget?" + data,
            cache: false,
            type: "GET"
        })
            .done(() => {
            // call succeeded
        })
            .fail(() => {
            // call failed
        })
            .always(() => {
            // happens after done/fail on every call
        });
    }
}
exports.CameraHelper = CameraHelper;
