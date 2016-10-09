import { Distance } from "../Motion/Distance";
import { ISphere as Sphere } from "../../Interfaces/ISphere";
import { OrbitingCelestialObjectBase } from "../Base/OrbitingCelestialObjectBase";

export class Moon extends OrbitingCelestialObjectBase implements Sphere {
    Radius: Distance;
    AxialTilt: number;
    RotationSpeed: number;
    Volume: number;
}