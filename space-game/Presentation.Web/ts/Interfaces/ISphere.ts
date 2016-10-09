import { Distance } from "../Classes/Motion/Distance";
import { I3DObject } from "./I3DObject";

export interface ISphere extends I3DObject {
    Radius: Distance;
    AxialTilt: number;
    RotationSpeed: number;
}