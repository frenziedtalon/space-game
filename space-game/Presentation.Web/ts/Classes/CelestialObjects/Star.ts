import { OrbitingCelestialObjectBase } from "../Base/OrbitingCelestialObjectBase";
import { ISphere } from "../../Interfaces/ISphere";
import { Distance } from "../Motion/Distance";

export class Star extends OrbitingCelestialObjectBase implements ISphere {
    Radius: Distance;
    AxialTilt: number;
    RotationSpeed: number;
    Volume: number;
}