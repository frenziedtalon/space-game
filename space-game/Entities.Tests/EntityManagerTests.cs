using NSubstitute;
using NUnit.Framework;
using System;

namespace Entities.Tests
{
    [TestFixture]
    public class EntityManagerTests
    {
        [TestCase(1, 1)]
        [TestCase(5, 5)]
        [TestCase(20, 20)]
        public void RegisterEntity_WhenCalled_StoresEntity(int count, int expected)
        {
            EntityManager entityManager = new EntityManager();

            for (int i = 1; i <= count; i++)
            {
                BaseGameEntity entity = Substitute.For<BaseGameEntity>(entityManager);
            }

            Assert.AreEqual(expected, entityManager.Count);
        }

        [Test]
        public void RegisterEntity_WhenCalledWithDuplicate_Throws()
        {
            EntityManager entityManager = new EntityManager();
            Type expected = typeof(ArgumentException);

            BaseGameEntity entity = Substitute.For<BaseGameEntity>(entityManager);
            Exception ex = Assert.Catch<Exception>(() => entityManager.RegisterEntity(entity));

            Assert.IsInstanceOf(expected, ex);
        }

        [TestCase(null)]
        public void GetEntityFromId_WhenIdIsNothing_ReturnsNothing(Guid id)
        {
            EntityManager entityManager = new EntityManager();

            BaseGameEntity entity = entityManager.GetEntityFromId(id);

            Assert.AreSame(entity, null);
        }

        [Test]
        public void GetEntityFromId_WhenNoEntityForId_ReturnsNothing()
        {
            EntityManager entityManager = new EntityManager();
            Guid id = new Guid();

            BaseGameEntity entity = entityManager.GetEntityFromId(id);

            Assert.AreSame(entity, null);
        }

        [Test]
        public void GetEntityFromId_WhenIdValid_ReturnsEntity()
        {
            EntityManager entityManager = new EntityManager();

            BaseGameEntity entity = Substitute.For<BaseGameEntity>(entityManager);
            BaseGameEntity copyEntity = entityManager.GetEntityFromId(entity.Id);

            Assert.AreSame(entity, copyEntity);
        }
    }
}
