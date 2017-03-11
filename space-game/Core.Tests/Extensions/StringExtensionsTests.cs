using Core.Extensions;
using NUnit.Framework;

namespace Core.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("One", TestEnum.One)]
        [TestCase("two", TestEnum.Two)]
        [TestCase("three", TestEnum.Three)]
        public void ToEnum_WhenCalledWithValidValue_ReturnsCorrectValue(string testValue, TestEnum expectedValue)
        {
            TestEnum? result = testValue.ToEnum<TestEnum>();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedValue, result.Value);
        }

        [TestCase("Four")]
        [TestCase("")]
        public void ToEnum_WhenCalledWithInvalidValue_ReturnsNothing(string value)
        {
            TestEnum? result = value.ToEnum<TestEnum>();

            Assert.IsNull(result);
        }

    }

    public enum TestEnum
    {
        One,
        Two,
        Three
    }
}
