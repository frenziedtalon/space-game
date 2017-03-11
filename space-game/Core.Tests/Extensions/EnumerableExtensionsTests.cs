using Core.Classes;
using Core.Enums;
using Core.Extensions;
using Core.Tests.Data;
using NUnit.Framework;
using System.Collections.Generic;

namespace Core.Tests.Extensions
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [TestCaseSource(typeof(EnumerableExtensionsTestsData), nameof(EnumerableExtensionsTestsData.HasAny_WhenGivenAList_ReturnsExpected_Data))]
        public void HasAny_WhenGivenAList_ReturnsExpected(List<string> list, bool expected)
        {
            bool result = list.HasAny();

            Assert.AreEqual(expected, result);
        }

        [TestCaseSource(typeof(EnumerableExtensionsTestsData), nameof(EnumerableExtensionsTestsData.GetHighestAvailableResolution_WhenGivenAList_ReturnsExpected_Data))]
        public void GetHighestAvailableResolution_WhenGivenAList_ReturnsExpected(List<Texture> textures, TextureType type, string expected)
        {
            string result = textures.GetHighestAvailableResolution(type);

            Assert.AreEqual(expected, result);
        }

        [TestCaseSource(typeof(EnumerableExtensionsTestsData), nameof(EnumerableExtensionsTestsData.GetLowestAvailableResolution_WhenGivenAList_ReturnsExpected_Data))]
        public void GetLowestAvailableResolution_WhenGivenAList_ReturnsExpected(List<Texture> textures, TextureType type, string expected)
        {
            string result = textures.GetLowestAvailableResolution(type);

            Assert.AreEqual(expected, result);
        }

        [TestCaseSource(typeof(EnumerableExtensionsTestsData), nameof(EnumerableExtensionsTestsData.GetHighestAvailableResolutionForEachType_High))]
        [TestCaseSource(typeof(EnumerableExtensionsTestsData), nameof(EnumerableExtensionsTestsData.GetHighestAvailableResolutionForEachType_Medium))]
        [TestCaseSource(typeof(EnumerableExtensionsTestsData), nameof(EnumerableExtensionsTestsData.GetHighestAvailableResolutionForEachType_Low))]
        [TestCaseSource(typeof(EnumerableExtensionsTestsData), nameof(EnumerableExtensionsTestsData.GetHighestAvailableResolutionForEachType_Mix))]
        [TestCaseSource(typeof(EnumerableExtensionsTestsData), nameof(EnumerableExtensionsTestsData.GetHighestAvailableResolutionForEachType_Empty))]
        public void GetHighestAvailableResolutionForEachType_WhenGivenAList_ReturnsExpected(List<Texture> textures, List<Texture> expected)
        {
            List<Texture> result = textures.GetHighestAvailableResolutionForEachType();

            Assert.AreEqual(expected, result);
        }

        [TestCaseSource(typeof(EnumerableExtensionsTestsData), nameof(EnumerableExtensionsTestsData.IsEquivalent_WhenGivenAList_ReturnsExpected_NullAndEmpty))]
        [TestCaseSource(typeof(EnumerableExtensionsTestsData), nameof(EnumerableExtensionsTestsData.IsEquivalent_WhenGivenAList_ReturnsExpected_Contents))]
        public void IsEquivalent_WhenGivenAList_ReturnsExpected(List<Texture> one, List<Texture> two, bool expected)
        {
            bool result = one.IsEquivalent(two);

            Assert.AreEqual(expected, result);
        }
    }
}
