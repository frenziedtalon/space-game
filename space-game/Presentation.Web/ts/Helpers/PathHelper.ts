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

    static generatePlanetaryRingsPaths(scaledInnerRadius: number, scaledOuterRadius: number): Array<Array<BABYLON.Vector3>> {
        const innerPath = this.generateCircularPath(scaledInnerRadius);
        const outerPath = this.generateCircularPath(scaledOuterRadius);

        const paths = [];
        paths.push(innerPath);
        paths.push(outerPath);

        return paths;
    }
}
