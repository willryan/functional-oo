using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MethodToDelegate;
using NUnit.Framework;

namespace FunctionalOO
{
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }
    }
    public static class CompositionExtensions
    {
        public static Func<TR, TT> Compose<TR, TS, TT>(this Func<TR, TS> f1, Func<TS, TT> f2)
        {
            return r => f2(f1(r));
        } 
    }

    public delegate double PointPythag(Point p);
    public class FuncComposition
    {
        public static Func<TX,TX> Identity<TX>() => x => x;

        public static PointPythag Pythag =
            Identity<Point>()
                .Compose(Square)
                .Compose(Add)
                .Compose(Math.Sqrt)
                .ToDelegate<PointPythag>();

        public static Point Square(Point p) => new Point(p.X*p.X, p.Y*p.Y);
        public static double Add(Point p) => p.X + p.Y;
    }

    [TestFixture]
    public class TestCompose
    {
        [Test]
        public void TestPythagCompose()
        {
            Assert.That(FuncComposition.Pythag(new Point(3.0, 4.0)), Is.EqualTo(5.0));
            Assert.That(FuncComposition.Pythag(new Point(8.0, 15.0)), Is.EqualTo(17.0));
        }
    }
}
