"use strict";

class CameraHelper {
    MainSceneCameraName = "mainSceneCamera";
    NavCameraNameEnd = "NavCamera";
    private navBarWidth = 0.6;
    private y = 0;
    private height = 0.2;

    private canvas: HTMLCanvasElement;
    private meshHelper: MeshHelper;

    private currentTargetId: string;

    constructor(private engine: BABYLON.Engine) {
        this.canvas = engine.getRenderingCanvas();
        this.meshHelper = new MeshHelper(this);
    }

    updateNavigationCameras(cameras: Array<BABYLON.Camera>, scene: BABYLON.Scene): void {
        const navCameraWidth = 0.2 * this.navBarWidth * 0.6;
        let navCamCount = 0;

        for (let i = 0; i < cameras.length; i++) {
            if (cameras[i].name.endsWith(this.NavCameraNameEnd)) {
                const c = cameras[i] as BABYLON.TargetCamera;
                const parent = scene.getMeshesByTags(c.id)[0];
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
        const offset = new BABYLON.Vector3(radius * unitVector.x, radius * unitVector.y, radius * unitVector.z);

        return target.position.add(offset);
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

        if (!(mesh === null || mesh === undefined)) {
            this.setCameraTarget(mesh, scene, userInitiated);
        }
    }

    userSelectedTarget(mesh: BABYLON.AbstractMesh, scene: BABYLON.Scene): void {
        this.setCameraTarget(mesh, scene, true);
        this.updateCameraTarget(mesh.id);
    }

    setCameraTarget(mesh: BABYLON.AbstractMesh, scene: BABYLON.Scene, userInitiated: boolean): void {
        if (!(mesh === null || mesh.id === this.currentTargetId) || (userInitiated === false)) {
            let limit = this.meshHelper.getMeshBoundingSphereRadius(mesh) * 1.5;

            if (limit < 1) {
                limit = 1;
            }

            const msc = this.getMainSceneCamera(scene);
            msc.lowerRadiusLimit = limit;

            this.currentTargetId = mesh.id;

            if (userInitiated) {
                this.flyCameraToPointAtTarget(msc, mesh);
            } else {
                // use parenting to calculate the wanted camera position
                msc.parent = mesh;

                let pos = msc.globalPosition;
                msc.parent = null;
                msc.position = pos;
                msc.target = mesh.position;
            }
        }
    }

    updateCameraTarget(targetId: string): void {
        const data = "target=" + targetId;

        $.ajax({
            url: Configuration.apiUrl() + "/Camera/SetTarget?" + data,
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

    getMainSceneCamera(scene: BABYLON.Scene): BABYLON.ArcRotateCamera {
        return scene.getCameraByName(this.MainSceneCameraName) as BABYLON.ArcRotateCamera;
    }

    private removeFromActiveCameras(camera: BABYLON.Camera): void {
        const scene = camera.getScene();
        for (let i = scene.activeCameras.length - 1; i >= 0; i--) {
            const c = scene.activeCameras[i];

            if (c.name === camera.name) {
                scene.activeCameras.splice(i, 1);
            }
        }
    }

    private switchActiveCameras(oldCamera: BABYLON.Camera, newCamera: BABYLON.Camera): void {
        const scene = oldCamera.getScene();

        newCamera.viewport = oldCamera.viewport;
        scene.activeCameras.push(newCamera);
        scene.activeCamera = newCamera;

        this.removeFromActiveCameras(oldCamera);
        this.setTargetCameraActive(scene.activeCameras, newCamera.name);
    }

    private flyCameraToPointAtTarget(camera: BABYLON.ArcRotateCamera, target: BABYLON.AbstractMesh): void {
        const newPosition = this.calculateMainCameraFlyToPositionOnTargetChange(camera, target.position);
        this.flyCameraToPosition(camera, newPosition, target.position);
    }

    private calculateMainCameraFlyToPositionOnTargetChange(camera: BABYLON.ArcRotateCamera, target: BABYLON.Vector3): BABYLON.Vector3 {
        const radius = - camera.lowerRadiusLimit;
        const unitVector = VectorHelper.calculateUnitVector(camera.globalPosition, target);
        return target.add(new BABYLON.Vector3(radius * unitVector.x, radius * unitVector.y, radius * unitVector.z));
    }

    private flyCameraToPosition(camera: BABYLON.ArcRotateCamera, position: BABYLON.Vector3, target: BABYLON.Vector3) {
        const scene = camera.getScene();
        AnimationHelper.removeAllAnimations(camera);

        // create a new camera in the target location. This will auto-calculate most of the parameters for us.
        const ghostCam = new BABYLON.ArcRotateCamera("ghostCam", 0, 0, 0, target, scene);
        ghostCam.setPosition(position);

        // remove ghostCam from camera list, we're not going to actually use it.
        scene.cameras.pop();

        const targetAnimation = new BABYLON.Animation("camTarget", "target", 60, BABYLON.Animation.ANIMATIONTYPE_VECTOR3, BABYLON.Animation.ANIMATIONLOOPMODE_CONSTANT);
        const radiusAnimation = new BABYLON.Animation("camRadius", "radius", 60, BABYLON.Animation.ANIMATIONTYPE_FLOAT, BABYLON.Animation.ANIMATIONLOOPMODE_CONSTANT);
        const alphaAnimation = new BABYLON.Animation("camAlpha", "alpha", 60, BABYLON.Animation.ANIMATIONTYPE_FLOAT, BABYLON.Animation.ANIMATIONLOOPMODE_CONSTANT);
        const betaAnimation = new BABYLON.Animation("camBeta", "beta", 60, BABYLON.Animation.ANIMATIONTYPE_FLOAT, BABYLON.Animation.ANIMATIONLOOPMODE_CONSTANT);

        // remove any full rotations
        const currentAlpha = camera.alpha % (Math.PI * 2);
        const currentBeta = camera.beta % (Math.PI * 2);

        const alpha = ghostCam.alpha % (Math.PI * 2);
        const beta = ghostCam.beta % (Math.PI * 2);

        const keysTarget = [{
            frame: 0,
            value: camera.target
        }, {
            frame: 300,
            value: ghostCam.target
        }];

        const keysRadius = [{
            frame: 0,
            value: camera.radius
        }, {
            frame: 300,
            value: ghostCam.radius
        }];

        const keysAlpha = [{
            frame: 0,
            value: currentAlpha
        }, {
            frame: 200,
            value: alpha
        }];

        const keysBeta = [{
            frame: 0,
            value: currentBeta
        }, {
            frame: 200,
            value: beta
        }];

        targetAnimation.setKeys(keysTarget);
        radiusAnimation.setKeys(keysRadius);
        alphaAnimation.setKeys(keysAlpha);
        betaAnimation.setKeys(keysBeta);

        const easingFunction = new BABYLON.BezierCurveEase(.47, .24, .27, .70);;

        targetAnimation.setEasingFunction(easingFunction);
        radiusAnimation.setEasingFunction(easingFunction);
        alphaAnimation.setEasingFunction(easingFunction);
        betaAnimation.setEasingFunction(easingFunction);

        camera.animations.push(targetAnimation);
        camera.animations.push(radiusAnimation);
        camera.animations.push(alphaAnimation);
        camera.animations.push(betaAnimation);

        camera.parent = null;

        scene.beginAnimation(camera, 0, 300, false, 1);
    }
}