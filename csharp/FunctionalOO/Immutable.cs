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
        public string Foo { get; }
        public int Bar { get; }
        public bool Baz { get; }

        public ImmData1(string foo, int bar, bool baz)
        {
            Foo = foo;
            Bar = bar;
            Baz = baz;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ImmData1);
        }

        protected bool Equals(ImmData1 other)
        {
            return other != null && string.Equals(Foo, other.Foo) && Bar == other.Bar && Baz == other.Baz;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Foo?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ Bar;
                hashCode = (hashCode*397) ^ Baz.GetHashCode();
                return hashCode;
            }
        }
    }

    public static class ImmutableExtensions
    {
        public static ImmData1 With(this ImmData1 orig, string foo)
        {
            return new ImmData1(foo, orig.Bar, orig.Baz);
        }
        public static ImmData1 With(this ImmData1 orig, int bar)
        {
            return new ImmData1(orig.Foo, bar, orig.Baz);
        }
        public static ImmData1 With(this ImmData1 orig, bool baz)
        {
            return new ImmData1(orig.Foo, orig.Bar, baz);
        }
    }

    [TestFixture]
    public class ImmutableTest
    {
        [Test]
        public void ChainingTest()
        {
            var obj = new ImmData1("hello", 1, true);
            var obj2 = obj.With(baz: false).With(foo: "yeah");
            Assert.That(obj, Is.Not.EqualTo(obj2));
            Assert.That(obj2, Is.EqualTo(new ImmData1("yeah", 1, false)));
        }
    }
}
