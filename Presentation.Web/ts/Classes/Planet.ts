class Planet extends BaseCelestialObject implements ISphere {
    Radius: number;
    AxialTilt: number;
    RotationSpeed: number;
    Volume: number;
    Orbit: Orbit;
    Moons: Array<Moon>;
}