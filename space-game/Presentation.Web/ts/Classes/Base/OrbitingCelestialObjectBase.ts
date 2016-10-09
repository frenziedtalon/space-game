import { BaseCelestialObject } from "./BaseCelestialObject";
import { IOrbitingObject } from "../../Interfaces/IOrbitingObject";
import { Orbit } from "../Motion/Orbit";

export class OrbitingCelestialObjectBase extends BaseCelestialObject implements IOrbitingObject {
    Orbit: Orbit;
}