using Core.Classes;
using NUnit.Framework;
using System.Collections.Generic;

namespace Core.Tests.Data
{
    public class AngleTestsData
    {
        public static List<TestCaseData> Equals_WhenComparing_ReturnsExpected_Data()
        {
            Angle nullAngle = null;
            Angle a = Angle.FromDegrees(123.456);
            Angle copyOfA = a;
            Angle notSame = Angle.FromDegrees(12);

            return new List<TestCaseData> {
                new TestCaseData(a, nullAngle, false),
                new TestCaseData(a, copyOfA, true),
                new TestCaseData(a, notSame, false)
            };
        }
    }
}
