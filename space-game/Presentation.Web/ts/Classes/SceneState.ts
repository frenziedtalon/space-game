import { BaseGameEntity } from "./Base/BaseGameEntity";
import { SceneScaling } from "./Scaling/SceneScaling";

export class SceneState {
    CelestialObjects: Array<BaseGameEntity>;
    Scaling: SceneScaling;
}