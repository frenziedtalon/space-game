class Planet extends OrbitingCelestialObjectBase implements ISphere {
    Radius: number;
    AxialTilt: number;
    RotationSpeed: number;
    Volume: number;
    Moons: Array<Moon>;
}