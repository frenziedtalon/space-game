using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TurnTracker.Tests
{
    [TestFixture]
    public class TurnTrackerTests
    {
        [TestCase(1, 1)]
        [TestCase(50, 50)]
        [TestCase(1000, 1000)]
        public void Update_WhenCalled_IncrementsTurnNumber(int turns, int expectedTurn)
        {
            // Arrange
            TurnTrackerMock service = new TurnTrackerMock();

            // Act
            for (int i = 1; i <= turns; i++)
            {
                service.Update();
            }

            // Assert
            Assert.AreEqual(expectedTurn, service.TurnNumber);
        }

        [Test]
        public void StartDate_WhenCalled_ReturnsExpectedValue()
        {
            TurnTrackerMock turnTracker = new TurnTrackerMock();
            DateTime expectedStartDate = new DateTime(1990, 1, 1);

            DateTime result = turnTracker.StartDate;

            Assert.AreEqual(expectedStartDate, result);
        }

        [TestCaseSource(nameof(CurrentDateTestCases))]
        public void CurrentDate_WhenUpdateCalled_ReturnsExpectedValue(TimeSpan turnLength, int turns, DateTime expectedCurrentDate)
        {
            TurnTrackerMock turnTracker = new TurnTrackerMock(turnLength);
            for (int i = 1; i <= turns; i++)
            {
                turnTracker.Update();
            }

            DateTime result = turnTracker.CurrentDate;

            Assert.AreEqual(expectedCurrentDate, result);
        }

        private static List<TestCaseData> CurrentDateTestCases => new List<TestCaseData>
        {
            new TestCaseData(TimeSpan.FromDays(1), 0, new DateTime(1990, 1, 1)),
            new TestCaseData(TimeSpan.FromDays(1), 10, new DateTime(1990, 1, 11)),
            new TestCaseData(TimeSpan.FromDays(1), 35, new DateTime(1990, 2, 5)),
            new TestCaseData(TimeSpan.FromDays(30), 5, new DateTime(1990, 5, 31)),
            new TestCaseData(TimeSpan.FromDays(30), 1000, new DateTime(2072, 2, 20))
        };

        private class TurnTrackerMock : TurnTrackerBase
        {
            public TurnTrackerMock(TimeSpan? turnLength = null) : base(turnLength)
            {
            }
        }
    }
}
