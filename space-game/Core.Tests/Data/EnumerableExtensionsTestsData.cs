using Core.Classes;
using Core.Enums;
using NUnit.Framework;
using System.Collections.Generic;

namespace Core.Tests.Data
{
    public class EnumerableExtensionsTestsData
    {
        public static List<TestCaseData> HasAny_WhenGivenAList_ReturnsExpected_Data()
        {
            List<string> nullList = null;
            List<string> emptyList = new List<string>();
            List<string> listWithMembers = new List<string> {
                "a",
                "b"
            };

            return new List<TestCaseData> {
                new TestCaseData(nullList, false),
                new TestCaseData(emptyList, false),
                new TestCaseData(listWithMembers, true)
            };
        }

        private static SampleTextures _samples;
        private static SampleTextures Samples
        {
            get
            {
                if (_samples == null)
                {
                    _samples = new SampleTextures(TextureType.Diffuse);
                }
                return _samples;
            }
        }

        private static readonly List<Texture> All = new List<Texture> {
            Samples.Low,
            Samples.Medium,
            Samples.High
        };
        private static readonly List<Texture> MediumHigh = new List<Texture> {
            Samples.Medium,
            Samples.High
        };
        private static readonly List<Texture> MediumLow = new List<Texture> {
            Samples.Medium,
            Samples.Low
        };
        private static readonly List<Texture> LowHigh = new List<Texture> {
            Samples.High,
            Samples.Low
        };

        private static readonly List<Texture> OnlyLow = new List<Texture> { Samples.Low };

        private static readonly List<Texture> None = new List<Texture>();
        private static readonly List<Texture> AllWithLowValue = new List<Texture> {
            Samples.Low,
            Samples.EmptyMedium,
            Samples.EmptyHigh
        };
        private static readonly List<Texture> AllWithHighalue = new List<Texture> {
            Samples.EmptyLow,
            Samples.EmptyMedium,
            Samples.High
        };
        public static List<TestCaseData> GetHighestAvailableResolution_WhenGivenAList_ReturnsExpected_Data()
        {
            return new List<TestCaseData> {
                new TestCaseData(All, Samples.Type, Samples.High.Path),
                new TestCaseData(MediumHigh, Samples.Type, Samples.High.Path),
                new TestCaseData(OnlyLow, Samples.Type, Samples.Low.Path),
                new TestCaseData(MediumLow, Samples.Type, Samples.Medium.Path),
                new TestCaseData(LowHigh, Samples.Type, Samples.High.Path),
                new TestCaseData(None, Samples.Type, ""),
                new TestCaseData(All, Samples.Type + 1, ""),
                new TestCaseData(AllWithLowValue, Samples.Type, Samples.Low.Path),
                new TestCaseData(AllWithHighalue, Samples.Type, Samples.High.Path)
            };
        }

        public static List<TestCaseData> GetLowestAvailableResolution_WhenGivenAList_ReturnsExpected_Data()
        {
            return new List<TestCaseData> {
                new TestCaseData(All, Samples.Type, Samples.Low.Path),
                new TestCaseData(MediumHigh, Samples.Type, Samples.Medium.Path),
                new TestCaseData(OnlyLow, Samples.Type, Samples.Low.Path),
                new TestCaseData(MediumLow, Samples.Type, Samples.Low.Path),
                new TestCaseData(LowHigh, Samples.Type, Samples.Low.Path),
                new TestCaseData(None, Samples.Type, ""),
                new TestCaseData(All, Samples.Type + 1, ""),
                new TestCaseData(AllWithLowValue, Samples.Type, Samples.Low.Path),
                new TestCaseData(AllWithHighalue, Samples.Type, Samples.High.Path)
            };
        }

        public static List<TestCaseData> GetHighestAvailableResolutionForEachType_High()
        {
            SampleTextures diffuseSamples = new SampleTextures(TextureType.Diffuse);
            SampleTextures bumpSamples = new SampleTextures(TextureType.Bump);
            SampleTextures emissiveSamples = new SampleTextures(TextureType.Emissive);

            List<Texture> textures = new List<Texture>();
            textures.AddRange(new List<Texture> {
                diffuseSamples.Low,
                diffuseSamples.Medium,
                diffuseSamples.High
            });
            textures.AddRange(new List<Texture> {
                bumpSamples.Low,
                bumpSamples.Medium,
                bumpSamples.High
            });
            textures.AddRange(new List<Texture> {
                emissiveSamples.Low,
                emissiveSamples.Medium,
                emissiveSamples.High
            });

            List<Texture> expected = new List<Texture> {
                diffuseSamples.High,
                bumpSamples.High,
                emissiveSamples.High
            };

            return new List<TestCaseData> { new TestCaseData(textures, expected) };
        }

        public static List<TestCaseData> GetHighestAvailableResolutionForEachType_Medium()
        {
            SampleTextures diffuseSamples = new SampleTextures(TextureType.Diffuse);
            SampleTextures bumpSamples = new SampleTextures(TextureType.Bump);
            SampleTextures emissiveSamples = new SampleTextures(TextureType.Emissive);

            List<Texture> textures = new List<Texture>();
            textures.AddRange(new List<Texture> {
                diffuseSamples.Low,
                diffuseSamples.Medium
            });
            textures.AddRange(new List<Texture> {
                bumpSamples.Low,
                bumpSamples.Medium
            });
            textures.AddRange(new List<Texture> {
                emissiveSamples.Low,
                emissiveSamples.Medium
            });

            List<Texture> expected = new List<Texture> {
                diffuseSamples.Medium,
                bumpSamples.Medium,
                emissiveSamples.Medium
            };

            return new List<TestCaseData> { new TestCaseData(textures, expected) };
        }

        public static List<TestCaseData> GetHighestAvailableResolutionForEachType_Low()
        {
            SampleTextures diffuseSamples = new SampleTextures(TextureType.Diffuse);
            SampleTextures bumpSamples = new SampleTextures(TextureType.Bump);
            SampleTextures emissiveSamples = new SampleTextures(TextureType.Emissive);

            List<Texture> textures = new List<Texture>();
            textures.Add(diffuseSamples.Low);
            textures.Add(bumpSamples.Low);
            textures.Add(emissiveSamples.Low);

            List<Texture> expected = new List<Texture> {
                diffuseSamples.Low,
                bumpSamples.Low,
                emissiveSamples.Low
            };

            return new List<TestCaseData> { new TestCaseData(textures, expected) };
        }

        public static List<TestCaseData> GetHighestAvailableResolutionForEachType_Mix()
        {
            SampleTextures diffuseSamples = new SampleTextures(TextureType.Diffuse);
            SampleTextures bumpSamples = new SampleTextures(TextureType.Bump);
            SampleTextures emissiveSamples = new SampleTextures(TextureType.Emissive);

            List<Texture> textures = new List<Texture>();
            textures.AddRange(new List<Texture> {
                diffuseSamples.Low,
                diffuseSamples.Medium,
                diffuseSamples.High
            });
            textures.AddRange(new List<Texture> {
                bumpSamples.Low,
                bumpSamples.Medium
            });
            textures.Add(emissiveSamples.Low);

            List<Texture> expected = new List<Texture> {
                diffuseSamples.High,
                bumpSamples.Medium,
                emissiveSamples.Low
            };

            return new List<TestCaseData> { new TestCaseData(textures, expected) };
        }

        public static List<TestCaseData> GetHighestAvailableResolutionForEachType_Empty()
        {
            SampleTextures diffuseSamples = new SampleTextures(TextureType.Diffuse);
            SampleTextures bumpSamples = new SampleTextures(TextureType.Bump);
            SampleTextures emissiveSamples = new SampleTextures(TextureType.Emissive);

            List<Texture> textures = new List<Texture>();
            diffuseSamples.High.Path = "";
            bumpSamples.Medium.Path = "";
            emissiveSamples.Low.Path = "";

            textures.AddRange(new List<Texture> {
                diffuseSamples.Low,
                diffuseSamples.Medium,
                diffuseSamples.High
            });
            textures.AddRange(new List<Texture> {
                bumpSamples.Low,
                bumpSamples.Medium
            });
            textures.Add(emissiveSamples.Low);

            List<Texture> expected = new List<Texture> {
                diffuseSamples.Medium,
                bumpSamples.Low
            };

            return new List<TestCaseData> { new TestCaseData(textures, expected) };
        }

        public static List<TestCaseData> IsEquivalent_WhenGivenAList_ReturnsExpected_NullAndEmpty()
        {
            return new List<TestCaseData> { new TestCaseData(new List<Texture>(), null, false) };
        }

        public static List<TestCaseData> IsEquivalent_WhenGivenAList_ReturnsExpected_Contents()
        {

            List<Texture> allCopy = new List<Texture>(All);
            List<Texture> allWithExtra = new List<Texture>(All);

            allWithExtra.Add(new Texture
            {
                QualityEnum = TextureQuality.Medium,
                TypeEnum = TextureType.Opacity,
                Path = "extra texture"
            });

            All.Reverse();

            return new List<TestCaseData> {
                new TestCaseData(All, All, true),
                new TestCaseData(allCopy, allCopy, true),
                new TestCaseData(allCopy, All, true),
                new TestCaseData(All, OnlyLow, false),
                new TestCaseData(All, allWithExtra, false)
            };
        }
    }
}
