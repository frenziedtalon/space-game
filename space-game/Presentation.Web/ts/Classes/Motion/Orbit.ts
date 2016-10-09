import { Distance } from "./Distance";
import { Angle } from "../Angle";

export class Orbit {
    Position: string;
    Eccentricity: number;
    PeriodDays: number;
    MeanAnomalyZero: Angle;
    SemiMajorAxis: Distance;
    LongitudeOfAscendingNode: Angle;
    ArgumentOfPeriapsis: Angle;
    Inclination: Angle;
}