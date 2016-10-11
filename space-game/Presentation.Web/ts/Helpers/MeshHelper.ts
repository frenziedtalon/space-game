"use strict";
class MeshHelper {
    constructor(private cameraHelper: CameraHelper) {
    }

    getMeshBoundingSphereRadius(mesh: BABYLON.AbstractMesh): number {
        return mesh._boundingInfo.boundingSphere.radius;
    }

    pickMesh(evt: MouseEvent, scene: BABYLON.Scene): void {
        // see if there's a mesh under the click
        const clientPoint = new BABYLON.Vector2(evt.x, evt.y);
        const pickResult = scene.pick(evt.clientX,
            evt.clientY,
            null,
            false,
            this.cameraHelper.getViewportCamera(clientPoint, scene.activeCameras));
        
        // if there is a hit and we can select the object then set it as the camera target
        if (pickResult.hit) {
	        this.cameraHelper.userSelectedTarget(pickResult.pickedMesh, scene);
        }
    }

    drawPath(meshName: string, path: Array<BABYLON.Vector3>, colour: BABYLON.Color3, scene: BABYLON.Scene): BABYLON.LinesMesh {
        const mesh: BABYLON.LinesMesh = BABYLON.Mesh.CreateLines(meshName, path, scene);
        mesh.color = colour;
        return mesh;
    }
}