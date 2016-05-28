using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monad;
using NUnit.Framework;

namespace FunctionalOO
{
    public class MonadLibrary
    {
        public static string DoSomeMath(double input)
        {
            var r = 
                from v in SucceedIfOverFive(input)
                from y in SucceedIfEven(v)
                select y;

            return r.Right();
        }

        private static Either<Exception, int> SucceedIfOverFive(double value)
        {
            if (value > 5.0)
                return () => (int) (value - 5.0);
            else
                return () => new Exception("too low");
        }

        private static Either<Exception, string> SucceedIfEven(int value)
        {
            if (value%2 == 0)
                return () => (value/2).ToString();
            else
                return () => new Exception("uneven");
            
        }
    }

    [TestFixture]
    public class TestMonadLibrary
    {
        [Test]
        public void TestEither()
        {
            Assert.That(MonadLibrary.DoSomeMath(13.2), Is.EqualTo("4"));
            Assert.That(() => MonadLibrary.DoSomeMath(3.2), Throws.Exception);
            Assert.That(() => MonadLibrary.DoSomeMath(12.2), Throws.Exception);
        }
    }
}
