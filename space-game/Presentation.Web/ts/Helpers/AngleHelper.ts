"use strict";

export class AngleHelper {
    static randomRadians(): number {
        return Math.random() * Math.PI * 2;
    }

    static randomRotationVector(): BABYLON.Vector3 {
        return new BABYLON.Vector3(this.randomRadians(), this.randomRadians(), this.randomRadians());
    }
}