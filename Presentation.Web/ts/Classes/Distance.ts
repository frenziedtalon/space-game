class Distance {
    private _kilometers: number;
    
    // not typesafe but currently typescript can't do proper overloading
    constructor(distance: Kilometer | AstronomicalUnit) {
        if (distance.hasOwnProperty("Kilometers")) {
            this._kilometers = (<Kilometer>distance).Kilometers;
        } else if (distance.hasOwnProperty("AstronomicalUnits")) {
            this._kilometers = (<AstronomicalUnit>distance).AstronomicalUnits * this.kilometersInAstronomicalUnit;
        }
    }

    private get kilometersInAstronomicalUnit(): number {
        return 149597870.691;
    }
    
    get AstronomicalUnits(): number {
        return this._kilometers / this.kilometersInAstronomicalUnit;
    }

    get Kilometers(): number {
        return this._kilometers;
    }
}