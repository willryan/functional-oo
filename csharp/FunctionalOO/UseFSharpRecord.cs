using Dependencies;
using NUnit.Framework;

namespace FunctionalOO
{
    [TestFixture]
    public class UseFSharpRecord
    {

        [Test]
        public void TestRecord()
        {
            var rec = new ImmRecord(x: "one", y: 2, z: false);
            Assert.That(rec.X, Is.EqualTo("one"));
            Assert.That(rec, Is.EqualTo(new ImmRecord("one", 2, false)));
        }
    }
}