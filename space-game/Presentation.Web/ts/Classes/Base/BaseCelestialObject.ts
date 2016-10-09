import { BaseGameEntity } from "./BaseGameEntity";
import { OrbitingCelestialObjectBase } from "./OrbitingCelestialObjectBase";
import { ICelestialObject } from "../../Interfaces/ICelestialObject";

export class BaseCelestialObject extends BaseGameEntity implements ICelestialObject {
    Mass: number;
    Name: string;
    Texture: string;
    Satellites: Array<OrbitingCelestialObjectBase>;
}