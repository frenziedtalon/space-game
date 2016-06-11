"use strict";
class OrbitalMechanics {
    generateOrbitPath(orbit: Orbit): Array<BABYLON.Vector3> {
        const result: Array<BABYLON.Vector3> = [];

        for (let i = 0; i < orbit.PeriodDays; i += 1) {
            result.push(this.calculateOrbitPosition(orbit, i));
        }

        if (orbit.Eccentricity < 1) {
            result.push(result[0]);
        }

        return result;
    }

    calculateOrbitPosition(orbit: Orbit, days: number): BABYLON.Vector3 {
        if (orbit.Eccentricity < 1) {
            return this.calculatePositionForEllipticalOrbit(orbit, days);
        } else if (orbit.Eccentricity === 1) {
            return this.calculatePositionForParabolicOrbit(orbit, days);
        } else {
            return this.calculatePositionForHyperbolicOrbit(orbit, days);
        }
    }

    calculatePositionForParabolicOrbit(orbit: Orbit, days: number): BABYLON.Vector3 {
        throw new Error("Not implemented");
    }

    calculatePositionForHyperbolicOrbit(orbit: Orbit, days: number): BABYLON.Vector3 {
        throw new Error("Not implemented");
    }

    calculatePositionForEllipticalOrbit(orbit: Orbit, days: number): BABYLON.Vector3 {

        const meanAnomaly: number = this.calculateMeanAnomaly(orbit, days);
        const eccentricAnomaly: number = this.calculateEccentricAnomaly(meanAnomaly);
        const trueAnomaly: number = this.calculateTrueAnomaly(eccentricAnomaly);
        const distance: number = this.calculateDistance(trueAnomaly);

        const x = this.calculateX(distance, trueAnomaly);
        const y = this.calculateY(distance, trueAnomaly);
        const z = this.calculateZ(distance, trueAnomaly);

        // TODO: scale the values

        return new BABYLON.Vector3(x, y, z);
    }

    calculateMeanAnomaly(orbit: Orbit, days: number): number {
        throw new Error("Not implemented");
    }

    calculateEccentricAnomaly(meanAnomaly: number): number {
        throw new Error("Not implemented");
    }

    calculateTrueAnomaly(eccentricAnomaly: number): number {
        throw new Error("Not implemented");
    }

    calculateDistance(trueAnomaly: number): number {
        throw new Error("Not implemented");
    }

    calculateX(distance: number, trueAnomaly: number): number {
        throw new Error("Not implemented");
    }

    calculateY(distance: number, trueAnomaly: number): number {
        throw new Error("Not implemented");
    }

    calculateZ(distance: number, trueAnomaly: number): number {
        throw new Error("Not implemented");
    }
}