"use strict";
class OrbitalMechanics {

    constructor(public ScaleSemiMajorAxisCallback: (kilometers: number) => number, public Orbit: Orbit) {
        this.ScaleSemiMajorAxisCallback = ScaleSemiMajorAxisCallback;
        this.Orbit = Orbit;
    }

    generateOrbitPath(): Array<BABYLON.Vector3> {
        const result: Array<BABYLON.Vector3> = [];

        for (let i = 0; i < this.Orbit.PeriodDays; i += 1) {
            result.push(this.calculateOrbitPosition(i));
        }

        if (this.Orbit.Eccentricity < 1) {
            result.push(result[0]);
        }

        return result;
    }

    calculateOrbitPosition(days: number): BABYLON.Vector3 {
        if (this.Orbit.Eccentricity < 1) {
            return this.calculatePositionForEllipticalOrbit(days);
        } else if (this.Orbit.Eccentricity === 1) {
            return this.calculatePositionForParabolicOrbit(days);
        } else {
            return this.calculatePositionForHyperbolicOrbit(days);
        }
    }

    calculatePositionForParabolicOrbit(days: number): BABYLON.Vector3 {
        throw new Error("Not implemented");
    }

    calculatePositionForHyperbolicOrbit(days: number): BABYLON.Vector3 {
        throw new Error("Not implemented");
    }

    calculatePositionForEllipticalOrbit(days: number): BABYLON.Vector3 {

        const meanAnomaly: Angle = this.calculateMeanAnomaly(days);
        const eccentricAnomaly: Angle = this.calculateEccentricAnomaly(meanAnomaly);
        const trueAnomaly: Angle = this.calculateTrueAnomaly(eccentricAnomaly);
        const distance: Distance = this.calculateDistance(trueAnomaly);

        const x = this.calculateX(distance, trueAnomaly);
        const y = this.calculateY(distance, trueAnomaly);
        const z = this.calculateZ(distance, trueAnomaly);
        
        return new BABYLON.Vector3(this.ScaleSemiMajorAxisCallback(x.Kilometers),
                                    this.ScaleSemiMajorAxisCallback(y.Kilometers),
                                    this.ScaleSemiMajorAxisCallback(z.Kilometers));
    }

    calculateMeanAnomaly(days: number): Angle {
        var r = this.Orbit.MeanAnomalyZero.Radians + (this.Orbit.MeanAngularMotion.Radians * days);
        return new Angle(r);
    }

    calculateEccentricAnomaly(meanAnomaly: Angle): Angle {
        const threshold = 0.001 * (Math.PI / 180);
        const maxIterations = 50;

        // initial guess
        let en: number = meanAnomaly.Radians;
        let en1: number = 0;
        if (this.Orbit.Eccentricity > 0.8) {
            en = Math.PI;
        }

        for (let j = 0; j < maxIterations; j++) {
            en1 = en - ((en - meanAnomaly.Radians - (this.Orbit.Eccentricity * Math.sin(en))) / (1 - (this.Orbit.Eccentricity * Math.cos(en))));

            if ((en1 - en) < threshold) {
                break;
            }
            en = en1;
        }
        return new Angle(0);
    }

    calculateTrueAnomaly(eccentricAnomaly: Angle): Angle {
        throw new Error("Not implemented");
    }

    calculateDistance(trueAnomaly: Angle): Distance {
        throw new Error("Not implemented");
    }

    calculateX(distance: Distance, trueAnomaly: Angle): Distance {
        throw new Error("Not implemented");
    }

    calculateY(distance: Distance, trueAnomaly: Angle): Distance {
        throw new Error("Not implemented");
    }

    calculateZ(distance: Distance, trueAnomaly: Angle): Distance {
        throw new Error("Not implemented");
    }
}