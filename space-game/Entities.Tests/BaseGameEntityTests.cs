using NSubstitute;
using NUnit.Framework;
using System;

namespace Entities.Tests
{
    [TestFixture]
    public class BaseGameEntityTests
    {
        [Test]
        public void ctor_WhenArgumentIsNull_ThrowsArgumentNullException()
        {
            IEntityManager entityManager = null;
            Type expectedType = typeof(ArgumentNullException);

            Exception ex = Assert.Catch<Exception>(() => Substitute.For<BaseGameEntity>(entityManager));

            Assert.AreEqual(ex.InnerException.GetType(), expectedType);
        }
    }
}
