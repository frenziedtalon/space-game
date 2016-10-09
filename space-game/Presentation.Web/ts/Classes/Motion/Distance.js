"use strict";
class Distance {
    // not typesafe but currently typescript can't do proper overloading
    constructor(distance) {
        if (distance.hasOwnProperty("Kilometers")) {
            this._kilometers = distance.Kilometers;
        }
        else if (distance.hasOwnProperty("AstronomicalUnits")) {
            this._kilometers = distance.AstronomicalUnits * this.kilometersInAstronomicalUnit;
        }
    }
    get kilometersInAstronomicalUnit() {
        return 149597870.691;
    }
    get AstronomicalUnits() {
        return this._kilometers / this.kilometersInAstronomicalUnit;
    }
    get Kilometers() {
        return this._kilometers;
    }
}
exports.Distance = Distance;
