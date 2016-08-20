"use strict";
class CameraHelper {
    static navCameraNameEnd = "NavCamera";
    static navBarWidth = 0.6;
    static y = 0;
    static height = 0.2;

    static updateNavigationCameras(cameras: Array<BABYLON.Camera>): void {
        const navCameraWidth = 0.2 * this.navBarWidth * 0.6;
        let navCamCount = 0;

        for (let i = 0; i < cameras.length; i++) {
            if (cameras[i].name.endsWith(this.navCameraNameEnd)) {
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

    private static calculateNavigationCameraPosition(target: BABYLON.Mesh): BABYLON.Vector3 {
        const radius = - MeshHelper.getMeshBoundingSphereRadius(target) * 4;
        let pointA = BABYLON.Vector3.Zero();

        const targetParent = target.parent as BABYLON.Mesh;
        if (targetParent) {
            pointA = targetParent.position;
        }

        const unitVector = VectorHelper.calculateUnitVector(pointA, target.position);
        return new BABYLON.Vector3(radius * unitVector.x, radius * unitVector.y, radius * unitVector.z);
    }
}