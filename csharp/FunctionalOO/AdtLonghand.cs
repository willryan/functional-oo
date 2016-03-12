using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FunctionalOO
{
    public class Adt
    {
        private enum Tag
        {
            Foo,
            Bar,
            Baz
        }

        private readonly Tag _tag;
        private readonly int _foo;
        private readonly string _bar;

        private Adt(int foo)
        {
            _tag = Tag.Foo;
            _foo = foo;
        }

        private Adt(string bar)
        {
            _tag = Tag.Bar;
            _bar = bar;
        }

        private Adt()
        {
            _tag = Tag.Baz;
        }

        public static Adt NewFoo(int value)
        {
            return new Adt(value);            
        }
        public static Adt NewBar(string value)
        {
            return new Adt(value);            
        }
        public static Adt Baz = new Adt();

        public T Match<T>(Func<int,T> foo = null, Func<string, T> bar = null, Func<T> baz = null, Func<T> deflt = null)
        {
            var df = deflt ?? (() => { throw new ArgumentOutOfRangeException(); });
            switch (_tag)
            {
                case Tag.Foo:
                    return foo != null ? foo(_foo) : df();
                case Tag.Bar:
                    return bar != null ? bar(_bar) : df();
                case Tag.Baz:
                    return baz != null ? baz() : df();
                default:
                    return df();
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Adt);
        }

        protected bool Equals(Adt other)
        {
            return other != null && _tag == other._tag && _foo == other._foo && string.Equals(_bar, other._bar);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) _tag;
                hashCode = (hashCode*397) ^ _foo;
                hashCode = (hashCode*397) ^ (_bar != null ? _bar.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    public class UseAdt
    {
        public static string GimmeAString(Adt v)
        {
            return v.Match(foo: x => x.ToString(), deflt: () => "not a foo");
        }
        public static string AllCases(Adt v)
        {
            return v.Match(foo: x => x.ToString(), 
                bar: s => s.ToUpper(),
                baz: () => "BAZzZz!",
                deflt: () => "(default)");
        }
    }

    [TestFixture]
    public class TestHandRolledAdt
    {
        [Test]
        public void TestMatches()
        {
            var adt1 = Adt.NewFoo(3);
            var adt2 = Adt.NewBar("three");
            var adt3 = Adt.Baz;

            Assert.That(UseAdt.GimmeAString(adt1), Is.EqualTo("3"));
            Assert.That(UseAdt.GimmeAString(adt2), Is.EqualTo("not a foo"));
            Assert.That(UseAdt.GimmeAString(adt3), Is.EqualTo("not a foo"));

            Assert.That(UseAdt.AllCases(adt1), Is.EqualTo("3"));
            Assert.That(UseAdt.AllCases(adt2), Is.EqualTo("THREE"));
            Assert.That(UseAdt.AllCases(adt3), Is.EqualTo("BAZzZz!"));
        }
    }
}
