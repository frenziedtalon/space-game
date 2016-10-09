"use strict";
class VectorHelper {
    static calculateUnitVector(pointA, pointB) {
        const vector = pointB.subtract(pointA);
        const length = this.calculateVectorLength(vector);
        if (length === 0) {
            return new BABYLON.Vector3(1, 1, 1);
        }
        return new BABYLON.Vector3(vector.x / length, vector.y / length, vector.z / length);
    }
    static calculateVectorLength(vector) {
        return Math.sqrt((vector.x * vector.x) + (vector.y * vector.y) + (vector.z * vector.z));
    }
}
exports.VectorHelper = VectorHelper;
