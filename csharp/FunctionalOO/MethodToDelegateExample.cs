using System;
using MethodToDelegate;
using Moq;
using NUnit.Framework;

namespace FunctionalOO
{
    public delegate string PythagDisplay(double x, double y);

    public static class DelegateExample
    {
        public delegate double Add(double x, double y);
        public delegate double Mult(double x, double y);
        public delegate double Sqrt(double x);
        public delegate double Pythag(double x, double y);
        public delegate string DoubleToString(double x);

        [ToDelegate(typeof(Add))]
        public static double Addr(double a, double b)
        {
            return a + b;
        }

        [ToDelegate(typeof(Mult))]
        public static double Multr(double a, double b)
        {
            return a * b;
        }

        [ToDelegate(typeof(Sqrt))]
        public static double Sqrtr(double a)
        {
            return Math.Sqrt(a);
        }

        [ToDelegate(typeof(Pythag))]
        public static double Pythagr(Add add, Mult mult, Sqrt sqrt, double x, double y)
        {
            return sqrt(add(mult(x, x), mult(y, y)));
        }

        [ToDelegate(typeof (DoubleToString))]
        public static string DoubleToStringr(double x)
        {
            return x.ToString();
        }

        [ToDelegate(typeof(PythagDisplay))]
        public static string PythagD(Pythag pyt, DoubleToString itos, double x, double y)
        {
            return itos(pyt(x, y));
        }
    }

    [TestFixture]
    public class DelegateExampleTest
    {

        [Test]
        public void Test_Addr()
        {
            Assert.That(DelegateExample.Addr(3.0, 4.0), Is.EqualTo(7.0));
        }

        [Test]
        public void Test_Pythag()
        {
            var mockAdd = new Mock<DelegateExample.Add>();
            var mockMult = new Mock<DelegateExample.Mult>();
            var mockSqrt = new Mock<DelegateExample.Sqrt>();
            mockMult.Setup(m => m(1.0, 1.0)).Returns(11.0);
            mockMult.Setup(m => m(2.0, 2.0)).Returns(22.0);
            mockAdd.Setup(a => a(11.0, 22.0)).Returns(1212.0);
            mockSqrt.Setup(s => s(1212.0)).Returns(3.0);
            Assert.That(DelegateExample.Pythagr(mockAdd.Object, mockMult.Object, mockSqrt.Object, 1.0, 2.0), Is.EqualTo(3.0));
        }

        [Test]
        public void Test_PythagDisplay()
        {
            var mockPythag = new Mock<DelegateExample.Pythag>();
            var mockDoubleToString = new Mock<DelegateExample.DoubleToString>();
            mockPythag.Setup(p => p(1.0, 18.0)).Returns(81.0);
            mockDoubleToString.Setup(d => d(81.0)).Returns("Eighty one");
            Assert.That(DelegateExample.PythagD(mockPythag.Object, mockDoubleToString.Object, 1.0, 18.0), Is.EqualTo("Eighty one"));
        }

        [Test]
        public void Test_PythagDisplay_NoMocks()
        {
            DelegateExample.Pythag pythagImpl = (x, y) => x + y;
            DelegateExample.DoubleToString dTosTmpl = d => $"Got {d}";
            Assert.That(DelegateExample.PythagD(pythagImpl, dTosTmpl, 1.0, 18.0), Is.EqualTo("Got 19"));
        }
    }
}
