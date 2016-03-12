using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OneOf;

namespace FunctionalOO
{
    public class Unit
    {
        private Unit() { }

        public static Unit Singleton = new Unit();
    }

    public class UseOneOf
    {
        public static string GimmeAString(OneOf<int, string, Unit> v)
        {
            return v.Match(x => x.ToString(), 
                _ => "not a foo", 
                _ => "not a foo");
        }
        public static string AllCases(OneOf<int, string, Unit> v)
        {
            return v.Match(x => x.ToString(), 
                s => s.ToUpper(),
                _ => "BAZzZz!");
        }
    }

    [TestFixture]
    public class TestOneOf
    {
        [Test]
        public void TestMatches()
        {
            OneOf<int, string, Unit> adt1 = 3;
            OneOf<int, string, Unit> adt2 = "three";
            OneOf<int, string, Unit> adt3 = Unit.Singleton;

            Assert.That(UseOneOf.GimmeAString(adt1), Is.EqualTo("3"));
            Assert.That(UseOneOf.GimmeAString(adt2), Is.EqualTo("not a foo"));
            Assert.That(UseOneOf.GimmeAString(adt3), Is.EqualTo("not a foo"));

            Assert.That(UseOneOf.AllCases(adt1), Is.EqualTo("3"));
            Assert.That(UseOneOf.AllCases(adt2), Is.EqualTo("THREE"));
            Assert.That(UseOneOf.AllCases(adt3), Is.EqualTo("BAZzZz!"));
        }
    }
}
