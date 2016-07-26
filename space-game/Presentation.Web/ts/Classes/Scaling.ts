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

    get MaxDistance(): number {
        return this.SemiMajorAxisKilometerScaleFactor * this.Bounds.SemiMajorAxis.UpperBound.Kilometers;
    }

    get SkyBoxSize(): number {
        return this.MaxDistance * 2.5;
    }

    get CameraClippingDistance(): number {
        return this.MaxDistance * 3.5;
    }
}