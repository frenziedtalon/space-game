using Core.Helpers;
using NUnit.Framework;
using System;

namespace Core.Tests.Helpers
{
    [TestFixture]
    public class PathHelperTests
    {
        [TestCase("low/path/", "/Low.jpg", "low/path/Low.jpg")]
        [TestCase("low/path/", "Low.jpg", "low/path/Low.jpg")]
        [TestCase("low/path//", "Low.jpg", "low/path/Low.jpg")]
        public void PathCombine_WithValidInput_ReturnsExpected(string path1, string path2, string expected)
        {
            string result = PathHelper.SitePathCombine(path1, path2);

            Assert.AreEqual(expected, result);
        }

        [TestCase(null, null)]
        [TestCase(null, "")]
        [TestCase("", null)]
        [TestCase("", "12345")]
        [TestCase("12345", "")]
        [TestCase(null, "12345")]
        [TestCase("12345", null)]
        public void PathCombine_WithInvalidInput_ThrowsArgumentNullException(string path1, string path2)
        {
            Type expected = typeof(ArgumentNullException);

            ArgumentNullException ex = Assert.Catch<ArgumentNullException>(() => PathHelper.SitePathCombine(path1, path2));

            Assert.AreEqual(expected, ex.GetType());
        }
    }
}
