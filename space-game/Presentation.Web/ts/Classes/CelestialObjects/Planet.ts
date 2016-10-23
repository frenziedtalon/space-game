class Planet extends OrbitingCelestialObjectBase implements ISphere {
    Radius: Distance;
    AxialTilt: number;
    RotationSpeed: number;
    Volume: number;
    Moons: Array<Moon>;
}