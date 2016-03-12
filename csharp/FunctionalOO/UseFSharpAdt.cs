using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dependencies;
using NUnit.Framework;

namespace FunctionalOO
{
    [TestFixture]
    public class UseFSharpAdt
    {
        [Test]
        public void TestAdt()
        {
            var foo = EasyAdt.NewFoo(5);
            var bar = EasyAdt.NewBar("hello");
            var baz = EasyAdt.Baz;

            Assert.That(GimmeAString(foo), Is.EqualTo("5"));
            Assert.That(GimmeAString(bar), Is.EqualTo("not a FOO"));
            Assert.That(GimmeAString(baz), Is.EqualTo("not a FOO"));

            Assert.That(AllCases(foo), Is.EqualTo("5"));
            Assert.That(AllCases(bar), Is.EqualTo("HELLO"));
            Assert.That(AllCases(baz), Is.EqualTo("Bazazaz"));

        }

        private static string GimmeAString(EasyAdt v)
        {
            switch (v.Tag)
            {
                case EasyAdt.Tags.Foo:
                    return ((EasyAdt.Foo) v).Item.ToString();
                default:
                    return "not a FOO";
            }
        }
        private static string AllCases(EasyAdt v)
        {
            switch (v.Tag)
            {
                case EasyAdt.Tags.Foo:
                    return ((EasyAdt.Foo) v).Item.ToString();
                case EasyAdt.Tags.Bar:
                    return ((EasyAdt.Bar) v).Item.ToUpper();
                case EasyAdt.Tags.Baz:
                    return "Bazazaz";
                default:
                    return "(default)";
            }
        }
    }
}
