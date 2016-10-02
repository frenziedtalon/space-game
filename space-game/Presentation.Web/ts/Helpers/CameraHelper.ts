"use strict";
class CameraHelper {
    MainSceneCameraName = "mainSceneCamera";
    NavCameraNameEnd = "NavCamera";
    private navBarWidth = 0.6;
    private y = 0;
    private height = 0.2;

    private canvas: HTMLCanvasElement;
    private meshHelper: MeshHelper;

    constructor(private engine: BABYLON.Engine) {
        this.canvas = engine.getRenderingCanvas();
        this.meshHelper = new MeshHelper(this);
    }

    updateNavigationCameras(cameras: Array<BABYLON.Camera>): void {
        const navCameraWidth = 0.2 * this.navBarWidth * 0.6;
        let navCamCount = 0;

        for (let i = 0; i < cameras.length; i++) {
            if (cameras[i].name.endsWith(this.NavCameraNameEnd)) {
                const c = cameras[i] as BABYLON.TargetCamera;
                const parent = c.parent as BABYLON.Mesh;
                const x = navCamCount * navCameraWidth;
                c.viewport = new BABYLON.Viewport(x, this.y, navCameraWidth, this.height);
                c.position = this.calculateNavigationCameraPosition(parent);
                c.setTarget(parent.position);
                navCamCount += 1;
            }
        }
    }

    private calculateNavigationCameraPosition(target: BABYLON.Mesh): BABYLON.Vector3 {
        const radius = - this.meshHelper.getMeshBoundingSphereRadius(target) * 4;
        let pointA = BABYLON.Vector3.Zero();

        const targetParent = target.parent as BABYLON.Mesh;
        if (targetParent) {
            pointA = targetParent.position;
        }

        const unitVector = VectorHelper.calculateUnitVector(pointA, target.position);
        return new BABYLON.Vector3(radius * unitVector.x, radius * unitVector.y, radius * unitVector.z);
    }

    // Moves the main scene camera to be last in the activeCameras list which is the only way I've found so far to make the god rays work
    setMainSceneCameraActive(cameras: Array<BABYLON.Camera>): void {
        this.setTargetCameraActive(cameras, this.MainSceneCameraName);
    }

    setTargetCameraActive(cameras: Array<BABYLON.Camera>, name: string): void {
        for (let i = 0; i < cameras.length; i++) {
            if (cameras[i].name === name) {
                const c = cameras[i];
                cameras.splice(i, 1);
                cameras.push(c);
                return;
            }
        }
    }

    convertCanvasToViewportPoint(clientPoint: BABYLON.Vector2, hardwareScalingLevel: number): BABYLON.Vector2 {
        // canvas - (0,0) is top left
        // viewport - (0,0) is bottom left
        // invert y-axis for picked position
        const y = this.canvas.height - (clientPoint.y / hardwareScalingLevel);
        const x = clientPoint.x / hardwareScalingLevel;

        return new BABYLON.Vector2(x, y);
    }

    getViewportCamera(clientPoint: BABYLON.Vector2, cameras: Array<BABYLON.Camera>): BABYLON.Camera {
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

    clickedInViewPort(viewportPoint: BABYLON.Vector2, camera: BABYLON.Camera): boolean {
        const vp = camera.viewport;

        if ((viewportPoint.x > vp.x * this.canvas.width) && (viewportPoint.x < (vp.x + vp.width) * this.canvas.width)) {
            if ((viewportPoint.y > vp.y * this.canvas.height) && (viewportPoint.y < (vp.y + vp.height) * this.canvas.height)) {
                return true;
            }
        }
        return false;
    }

    setCameraTargetFromId(id: string, scene: BABYLON.Scene, userInitiated: boolean): void {
        const mesh = scene.getMeshByID(id);
        this.setCameraTarget(mesh, scene, userInitiated);
    }

    setCameraTarget(mesh: BABYLON.AbstractMesh, scene: BABYLON.Scene, userInitiated: boolean): void {
        if (!(mesh === null)) {
            let limit = this.meshHelper.getMeshBoundingSphereRadius(mesh) * 1.5;

            if (limit < 1) {
                limit = 1;
            }

            const mainSceneCamera = scene.getCameraByName(this.MainSceneCameraName) as BABYLON.ArcRotateCamera;

            mainSceneCamera.lowerRadiusLimit = limit;
            mainSceneCamera.parent = mesh;

            if (userInitiated) {
            }
        }
    }

    updateCameraTarget(targetId: string): void {
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