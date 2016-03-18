using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FunctionalOO
{
    public class ImmData1
    {
        public string X { get; }
        public int Y { get; }
        public bool Z { get; }

        public ImmData1(string x, int y, bool z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ImmData1);
        }

        protected bool Equals(ImmData1 other)
        {
            return other != null && string.Equals(X, other.X) && Y == other.Y && Z == other.Z;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ Y;
                hashCode = (hashCode*397) ^ Z.GetHashCode();
                return hashCode;
            }
        }
    }

    public static class ImmutableExtensions
    {
        public static ImmData1 With(this ImmData1 orig, string x)
        {
            return new ImmData1(x, orig.Y, orig.Z);
        }
        public static ImmData1 With(this ImmData1 orig, int y)
        {
            return new ImmData1(orig.X, y, orig.Z);
        }
        public static ImmData1 With(this ImmData1 orig, bool z)
        {
            return new ImmData1(orig.X, orig.Y, z);
        }
    }

    [TestFixture]
    public class ImmutableTest
    {
        [Test]
        public void ChainingTest()
        {
            var obj = new ImmData1("hello", 1, true);
            var obj2 = obj.With(z: false).With(x: "yeah");
            Assert.That(obj, Is.Not.EqualTo(obj2));
            Assert.That(obj2, Is.EqualTo(new ImmData1("yeah", 1, false)));
        }
    }
}
