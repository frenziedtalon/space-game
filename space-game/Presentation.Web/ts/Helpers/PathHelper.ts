"use strict";
class PathHelper {
    static generateCircularPath(radius: number): Array<BABYLON.Vector3> {
        const result: Array<BABYLON.Vector3> = [];
        const step = Math.PI / 128;

        for (let angle = 0; angle < 2 * Math.PI; angle += step) {
            result.push(CoordinateHelper.calculationParametricCoordinatesCircle(angle, radius));
        }

        return result;
    }
}
