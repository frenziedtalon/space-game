"use strict";
class MeshHelper {
    static getMeshBoundingSphereRadius(mesh: BABYLON.AbstractMesh): number {
        return mesh._boundingInfo.boundingSphere.radius;
    }
}