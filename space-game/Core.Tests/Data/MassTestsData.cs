using Core.Classes;
using NUnit.Framework;
using System.Collections.Generic;

namespace Core.Tests.Data
{
    public class MassTestsData
    {
        public static List<TestCaseData> Equals_WhenComparing_ReturnsExpected_Data()
        {
            Mass nullMass = null;
            Mass m = Mass.FromKilograms(12345);
            Mass copyOfM = m;
            Mass notSame = Mass.FromKilograms(123456);

            return new List<TestCaseData> {
                new TestCaseData(m, nullMass, false),
                new TestCaseData(m, copyOfM, true),
                new TestCaseData(m, notSame, false)
            };
        }
    }
}
