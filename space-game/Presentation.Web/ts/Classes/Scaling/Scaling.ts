﻿class Scaling {
    RadiusKilometerScaleFactor: number = 1;
    SemiMajorAxisKilometerScaleFactor: number = 1;
    Bounds: SceneScaling;

    private smallestRadius = 0.15; // this is the smallest size that can be successfully picked

    constructor(bounds: SceneScaling) {
        this.Bounds = bounds;

        const ratio = 10 * bounds.SemiMajorAxis.LowerBound.Kilometers / bounds.CelestialObjectRadius.UpperBound.Kilometers;

        this.RadiusKilometerScaleFactor = (this.smallestRadius / bounds.CelestialObjectRadius.LowerBound.Kilometers);

        this.SemiMajorAxisKilometerScaleFactor = ratio / bounds.SemiMajorAxis.LowerBound.Kilometers;
    }

    get maxDistance(): number {
        return this.SemiMajorAxisKilometerScaleFactor * this.Bounds.SemiMajorAxis.UpperBound.Kilometers;
    }
    
    get innerSkySphereDiameter(): number {
        return this.maxDistance * 4;
    }

    get outerSkySphereDiameter(): number {
        return this.innerSkySphereDiameter * 2;
    }

    get cameraClippingDistance(): number {
        return this.outerSkySphereDiameter * 1.1;
    }
}