class Scaling {
    RadiusKilometerScaleFactor: number = 1;
    SemiMajorAxisKilometerScaleFactor: number = 1;
    Bounds: SceneScaling;

    constructor(bounds: SceneScaling) {
        this.Bounds = bounds;

        const ratio = bounds.SemiMajorAxis.LowerBound.Kilometers / (bounds.CelestialObjectRadius.UpperBound.Kilometers * 5);

        this.RadiusKilometerScaleFactor = (0.1 / bounds.CelestialObjectRadius.LowerBound.Kilometers);

        this.SemiMajorAxisKilometerScaleFactor = (0.1 / bounds.SemiMajorAxis.LowerBound.Kilometers) / ratio;
    }

    get maxDistance(): number {
        return this.SemiMajorAxisKilometerScaleFactor * this.Bounds.SemiMajorAxis.UpperBound.Kilometers;
    }
    
    get innerSkySphereDiameter(): number {
        return this.maxDistance * 3;
    }

    get outerSkySphereDiameter(): number {
        return this.innerSkySphereDiameter * 2;
    }

    get cameraClippingDistance(): number {
        return this.outerSkySphereDiameter;
    }
}