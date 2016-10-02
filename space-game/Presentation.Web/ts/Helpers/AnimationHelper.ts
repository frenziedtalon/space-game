"use strict";
class AnimationHelper {
    static removeAllAnimations(node: BABYLON.Node) {
        if (node) {
            node.animations.splice(0, node.animations.length - 1);
        }
    }
}