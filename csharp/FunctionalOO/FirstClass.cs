using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace FunctionalOO
{
    public delegate double PerformSomeCalculation(double x, double y, bool condition);

    public class FirstClass
    {
        public static string UseADelegate(PerformSomeCalculation calc, double[] values)
        {
            return calc(values[0], values[1], values[2] > 0.0).ToString();
        }
    }

    [TestFixture]
    public class MockDelegate
    {
        [Test]
        public void Test_UseADelegate()
        {
            var pscMock = new Mock<PerformSomeCalculation>();
            pscMock.Setup(p => p(1.0, 2.0, true)).Returns(3.0);
            pscMock.Setup(p => p(3.0, 4.0, false)).Returns(6.0);
            Assert.That(FirstClass.UseADelegate(pscMock.Object, new[] {1.0, 2.0, 0.1}), Is.EqualTo("3"));
            Assert.That(FirstClass.UseADelegate(pscMock.Object, new[] {3.0, 4.0, -0.1}), Is.EqualTo("6"));
        }

    }
}
