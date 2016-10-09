"use strict";
class AngleHelper {
    static randomRadians() {
        return Math.random() * Math.PI * 2;
    }
    static randomRotationVector() {
        return new BABYLON.Vector3(this.randomRadians(), this.randomRadians(), this.randomRadians());
    }
}
exports.AngleHelper = AngleHelper;
