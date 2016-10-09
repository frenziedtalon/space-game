"use strict";
import { Orbit } from "./Orbit";
import { Distance } from "./Distance";
import { AstronomicalUnit } from "./AstronomicalUnit";
import { Kilometer } from "./Kilometer";
import { Angle } from "../Angle";

export class OrbitalMechanics {

    constructor(public ScaleSemiMajorAxisCallback: (kilometers: number) => number, public Orbit: Orbit) {
        this.ScaleSemiMajorAxisCallback = ScaleSemiMajorAxisCallback;
        this.Orbit = Orbit;
    }

    generateOrbitPath(): Array<BABYLON.Vector3> {
        const result: Array<BABYLON.Vector3> = [];
        const step = this.calculateOrbitPathStep();

        for (let i = 0; i < this.Orbit.PeriodDays; i += step) {
            result.push(this.calculateOrbitPosition(i));
        }

        if (this.Orbit.Eccentricity < 1) {
            result.push(result[0]);
        }

        return result;
    }

    calculateOrbitPathStep(): number {
        if (this.Orbit.PeriodDays < 30) {
            return 0.25;
        } else if (this.Orbit.PeriodDays < 60) {
            return 0.5;
        } else if (this.Orbit.PeriodDays > 1000) {
            return 5;
        } else {
            return 1;
        }
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

        return new BABYLON.Vector3(this.ScaleSemiMajorAxisCallback(y.Kilometers),
            this.ScaleSemiMajorAxisCallback(z.Kilometers),
            this.ScaleSemiMajorAxisCallback(x.Kilometers));
    }

    private _meanAngularMotion: Angle;
    meanAngularMotion(): Angle {
        if (this._meanAngularMotion === null || this._meanAngularMotion === undefined) {
            const r = (2 * Math.PI) / this.Orbit.PeriodDays;
            this._meanAngularMotion = new Angle(r);
        }
        return this._meanAngularMotion;
    }

    calculateMeanAnomaly(days: number): Angle {
        const r = this.Orbit.MeanAnomalyZero.Radians + (this.meanAngularMotion().Radians * days);
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
        return new Angle(en1);
    }

    calculateTrueAnomaly(eccentricAnomaly: Angle): Angle {
        const x = Math.sqrt(1 - this.Orbit.Eccentricity) * Math.cos(eccentricAnomaly.Radians / 2);
        const y = Math.sqrt(1 + this.Orbit.Eccentricity) * Math.sin(eccentricAnomaly.Radians / 2);
        const r = 2 * Math.atan2(y, x);
        return new Angle(r);
    }

    calculateDistance(trueAnomaly: Angle): Distance {
        const aus = (this.Orbit.SemiMajorAxis.AstronomicalUnits * (1 - (Math.pow(this.Orbit.Eccentricity, 2)))) / (1 + (this.Orbit.Eccentricity * (Math.cos(trueAnomaly.Radians))));
        return new Distance(new AstronomicalUnit(aus));
    }

    calculateX(distance: Distance, trueAnomaly: Angle): Distance {
        const kilometers = distance.Kilometers * ((Math.cos(this.Orbit.LongitudeOfAscendingNode.Radians) * Math.cos(trueAnomaly.Radians + this.Orbit.ArgumentOfPeriapsis.Radians)) - (Math.sin(this.Orbit.LongitudeOfAscendingNode.Radians) * Math.sin(trueAnomaly.Radians + this.Orbit.ArgumentOfPeriapsis.Radians))) * Math.cos(this.Orbit.Inclination.Radians);
        return new Distance(new Kilometer(kilometers));
    }

    calculateY(distance: Distance, trueAnomaly: Angle): Distance {
        const kilometers = distance.Kilometers * ((Math.sin(this.Orbit.LongitudeOfAscendingNode.Radians) * Math.cos(trueAnomaly.Radians + this.Orbit.ArgumentOfPeriapsis.Radians)) + (Math.cos(this.Orbit.LongitudeOfAscendingNode.Radians)) * Math.sin(trueAnomaly.Radians + this.Orbit.ArgumentOfPeriapsis.Radians)) * Math.cos(this.Orbit.Inclination.Radians);
        return new Distance(new Kilometer(kilometers));
    }

    calculateZ(distance: Distance, trueAnomaly: Angle): Distance {
        const kilometers = distance.Kilometers * Math.sin(trueAnomaly.Radians + this.Orbit.ArgumentOfPeriapsis.Radians) * Math.sin(this.Orbit.Inclination.Radians);
        return new Distance(new Kilometer(kilometers));
    }
}