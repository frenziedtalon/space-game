import { OrbitingCelestialObjectBase } from "../Base/OrbitingCelestialObjectBase";
import { ISphere } from "../../Interfaces/ISphere";
import { Distance } from "../Motion/Distance";
import { Moon } from "./Moon";

export class Planet extends OrbitingCelestialObjectBase implements ISphere {
    Radius: Distance;
    AxialTilt: number;
    RotationSpeed: number;
    Volume: number;
    Moons: Array<Moon>;
}