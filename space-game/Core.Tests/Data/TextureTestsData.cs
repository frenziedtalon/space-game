using Core.Classes;
using Core.Enums;
using NUnit.Framework;
using System.Collections.Generic;

namespace Core.Tests.Data
{
    public class TextureTestsData
    {
        public static List<TestCaseData> Equals_WhenComparing_ReturnsExpected_Data()
        {
            Texture standard = new Texture
            {
                TypeEnum = TextureType.Bump,
                Path = "path\\to\\texture",
                QualityEnum = TextureQuality.High
            };

            Texture copyOfStandard = standard;

            Texture capitalisedPath = new Texture
            {
                TypeEnum = TextureType.Bump,
                Path = "PATH\\TO\\TEXTURE",
                QualityEnum = TextureQuality.High
            };

            Texture emptyPath = new Texture
            {
                TypeEnum = TextureType.Bump,
                Path = "",
                QualityEnum = TextureQuality.High
            };

            Texture differentType = new Texture
            {
                TypeEnum = TextureType.Diffuse,
                Path = "path\\to\\texture",
                QualityEnum = TextureQuality.High
            };

            Texture differentQuality = new Texture
            {
                TypeEnum = TextureType.Bump,
                Path = "path\\to\\texture",
                QualityEnum = TextureQuality.Low
            };

            return new List<TestCaseData> {
                new TestCaseData(standard, copyOfStandard, true),
                new TestCaseData(standard, capitalisedPath, true),
                new TestCaseData(standard, emptyPath, false),
                new TestCaseData(standard, differentType, false),
                new TestCaseData(standard, differentQuality, false)
            };
        }
    }
}
