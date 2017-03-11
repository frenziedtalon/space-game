using Core.Classes;
using NUnit.Framework;
using System.Collections.Generic;

namespace Core.Tests.Data
{
    public class DistanceTestData
    {

        public static List<TestCaseData> Equals_WhenComparing_ReturnsExpected_Data()
        {
            Distance nullDistance = null;
            Distance d = Distance.FromKilometers(12345);
            Distance copyOfD = d;
            Distance notSame = Distance.FromKilometers(123456);

            return new List<TestCaseData> {
                new TestCaseData(d, nullDistance, false),
                new TestCaseData(d, copyOfD, true),
                new TestCaseData(d, notSame, false)
            };
        }

    }
}
