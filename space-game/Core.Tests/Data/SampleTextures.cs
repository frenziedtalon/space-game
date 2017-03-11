using Core.Classes;
using Core.Enums;

namespace Core.Tests.Data
{
    public class SampleTextures
    {
        public readonly Texture High;
        public readonly Texture Medium;
        public readonly Texture Low;
        public readonly Texture EmptyHigh;
        public readonly Texture EmptyMedium;
        public readonly Texture EmptyLow;

        public readonly TextureType Type;
        public SampleTextures(TextureType type)
        {
            Type = type;

            Low = new Texture
            {
                QualityEnum = TextureQuality.Low,
                TypeEnum = type,
                Path = "low texture path"
            };

            Medium = new Texture
            {
                QualityEnum = TextureQuality.Medium,
                TypeEnum = type,
                Path = "medium texture path"
            };

            High = new Texture
            {
                QualityEnum = TextureQuality.High,
                TypeEnum = type,
                Path = "high texture path"
            };


            EmptyLow = new Texture
            {
                QualityEnum = TextureQuality.Low,
                TypeEnum = type,
                Path = " "
            };

            EmptyMedium = new Texture
            {
                QualityEnum = TextureQuality.Medium,
                TypeEnum = type,
                Path = " "
            };

            EmptyHigh = new Texture
            {
                QualityEnum = TextureQuality.High,
                TypeEnum = type,
                Path = " "
            };
        }
    }
}
