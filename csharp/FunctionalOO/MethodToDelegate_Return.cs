using System;
using System.Linq;
using NUnit.Framework;

namespace FunctionalOO
{
    public delegate string PythagS(string x, string y);

    //[DI Attribute]
    public static class Funcs
    {
        public delegate double Pythag(double x, double y);
        public delegate double Add(double x, double y);
        public delegate double Mult(double x, double y);
        public delegate double Sqrt(double y);
        public delegate double ParseDouble(string x);
        public delegate string DoubleToString(double x);

        public static Add AddF() => (x, y) => x + y;
        public static Mult MultF => (x, y) => x * y;

        public static Sqrt SqrtF() => Math.Sqrt;

        public static ParseDouble ParseDoubleF() => Double.Parse;

        public static DoubleToString DoubleToStringF() => d => d.ToString();

        public static Pythag Pythagr(Add addF, Mult multF, Sqrt sqrtF) => 
            (double x, double y) => 
                sqrtF(addF(multF(x, x), multF(y, y)));

        public static PythagS PythagSr(Pythag py, ParseDouble parse, DoubleToString tos) => 
            (x, y) => 
                tos(py(parse(x), parse(y)));
    }



    [TestFixture]
    public class TestFromFuncs
    {
        [Test]
        public void TestDelegateIoc()
        {
            Funcs.Add mAdd = (x, y) => x + y;
            Funcs.Mult mMult = (x, y) => x * y;
            Funcs.Sqrt mSqrt = Math.Sqrt;
            var pythag = Funcs.Pythagr(mAdd, mMult, mSqrt);
            Assert.That(pythag(3, 4), Is.EqualTo(5.0));
        }
    }

}
