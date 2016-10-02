"use strict";
class VectorHelper {
    static calculateUnitVector(pointA: BABYLON.Vector3, pointB: BABYLON.Vector3): BABYLON.Vector3 {
        const vector = pointB.subtract(pointA);
        const length = this.calculateVectorLength(vector);
        if (length === 0) {
            return new BABYLON.Vector3(1, 1, 1);
        }
        return new BABYLON.Vector3(vector.x / length, vector.y / length, vector.z / length);
    }

    static calculateVectorLength(vector: BABYLON.Vector3): number {
        return Math.sqrt((vector.x * vector.x) + (vector.y * vector.y) + (vector.z * vector.z));
    }

    static calculateDistance(pointA: BABYLON.Vector3, pointB: BABYLON.Vector3): number {
        return this.calculateVectorLength(pointB.subtract(pointA));
    }
}