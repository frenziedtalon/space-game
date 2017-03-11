using Core.Classes;
using Core.Tests.Data;
using NUnit.Framework;

namespace Core.Tests.Classes
{
    public class TextureTests
    {
        [TestCaseSource(typeof(TextureTestsData), nameof(TextureTestsData.Equals_WhenComparing_ReturnsExpected_Data))]
        public void Equals_WhenComparing_ReturnsExpected(Texture @this, Texture that, bool expected)
        {
            bool result = @this.Equals(that);

            Assert.AreEqual(expected, result);
        }
    }
}
