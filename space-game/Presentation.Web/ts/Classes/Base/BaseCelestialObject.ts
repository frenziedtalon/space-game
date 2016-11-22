class BaseCelestialObject extends BaseGameEntity implements ICelestialObject {
    Mass: number;
    Name: string;
    Textures: Array<Texture>;
    Satellites: Array<OrbitingCelestialObjectBase>;
}