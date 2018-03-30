"use strict";
class NavigationCamera extends BABYLON.TargetCamera {
    constructor(target: BABYLON.Mesh, scene: BABYLON.Scene) {
        super(target.name + NavigationCamera.name, BABYLON.Vector3.Zero(), scene);
        
        this.setTarget(target.position);
        this.layerMask = 2; // 010 in binary

        // tag the target with this camera's id, for later retrieval
        BABYLON.Tags.AddTagsTo(target, this.createTagForTarget());
        BABYLON.Tags.AddTagsTo(target, this.getTypeName());

        scene.activeCameras.push(this);
    }

    getTypeName(): string {
        return NavigationCamera.name;
    }

    createTagForTarget(): string {
        return this.getTypeName() + this.id;
    }
}