using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FunctionalOO
{
    public class Expr
    {
        public static Expression<Func<double, double, double>> Pythag = (x, y) => Math.Sqrt((x*x) + (y*y));
    }

    [TestFixture]
    public class TestExprUse
    {
        [Test]
        public void TestCompile()
        {
            Assert.That(Expr.Pythag.Compile()(3.0, 4.0), Is.EqualTo(5.0));
        }

        [Test]
        public void TestAnalyze()
        {
            var expr = Expr.Pythag;
            var method = (expr.Body as MethodCallExpression).Method;
            var sqrtMethod = typeof(Math).GetMethod("Sqrt");
            Assert.That(method, Is.EqualTo(sqrtMethod));
        }

        [Test]
        public void GenerateIL()
        {
            throw new InconclusiveException("TODO");
        }
    }
}
