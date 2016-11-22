"use strict";

class TextureHelper {
    static getType(textures: Array<Texture>, type: TextureType): Texture {
        let result: Texture = null;
        if (!(textures === null || textures === undefined)) {
            const t = TextureType[type];

            for (let i = 0; i < textures.length; i++) {
                if (textures[i].Type === t) {
                    result = textures[i];
                    break;
                }
            }
        }
        return result;
    }
}