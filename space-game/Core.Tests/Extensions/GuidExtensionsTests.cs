using Core.Extensions;
using NUnit.Framework;
using System;

namespace Core.Tests.Extensions
{
    [TestFixture]
    public class GuidExtensionsTests
    {
        [Test]
        public void IsEmpty_WhenEmpty_ReturnsTrue()
        {
            Guid test = default(Guid);

            bool result = test.IsEmpty();

            Assert.AreEqual(true, result);
        }

        [Test]
        public void IsEmpty_WhenNotEmpty_ReturnsFalse()
        {
            Guid test = Guid.NewGuid();

            bool result = test.IsEmpty();

            Assert.AreEqual(false, result);
        }
    }
}
