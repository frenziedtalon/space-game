"use strict";
class Scaling {
    constructor(bounds) {
        this.RadiusKilometerScaleFactor = 1;
        this.SemiMajorAxisKilometerScaleFactor = 1;
        this.Bounds = bounds;
        const ratio = bounds.SemiMajorAxis.LowerBound.Kilometers / (bounds.CelestialObjectRadius.UpperBound.Kilometers * 5);
        this.RadiusKilometerScaleFactor = (0.1 / bounds.CelestialObjectRadius.LowerBound.Kilometers);
        this.SemiMajorAxisKilometerScaleFactor = (0.1 / bounds.SemiMajorAxis.LowerBound.Kilometers) / ratio;
    }
    get maxDistance() {
        return this.SemiMajorAxisKilometerScaleFactor * this.Bounds.SemiMajorAxis.UpperBound.Kilometers;
    }
    get innerSkySphereDiameter() {
        return this.maxDistance * 3;
    }
    get outerSkySphereDiameter() {
        return this.innerSkySphereDiameter * 2;
    }
    get cameraClippingDistance() {
        return this.outerSkySphereDiameter;
    }
}
exports.Scaling = Scaling;
