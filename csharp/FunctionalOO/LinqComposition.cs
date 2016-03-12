using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FunctionalOO
{
    public class LinqComposition
    {
        public static string[] CompositeFunction(double[] input)
        {
            return input.Select(Double)
                .Where(Filter)
                .OrderBy(v => v)
                .Select(Format("F2"))
                .ToArray();
        }

        public static double Double(double x) => x*2.0;
        public static bool Filter(double v) => v > 3.0;

        public static Func<double, string> Format(string formatStr) => 
            d => d.ToString(formatStr);
    }

    [TestFixture]
    public class TestComposite
    {
        [Test]
        public void Test_CompositeFunction()
        {
            Assert.That(LinqComposition.CompositeFunction(new [] { 1.0, 2.0, 3.0, 4.0 }), 
                Is.EqualTo(new [] { "4.00", "6.00", "8.00" }));
        }
    }
}
