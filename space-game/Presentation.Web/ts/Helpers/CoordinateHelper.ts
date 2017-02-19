"use strict";
class CoordinateHelper {
    static calculationParametricCoordinatesCircle(angle: number, radius: number): BABYLON.Vector3 {
        const z = radius * Math.cos(angle);
        const x = radius * Math.sin(angle);
        const y = 0;

        return new BABYLON.Vector3(x, y, z);
    }
}
