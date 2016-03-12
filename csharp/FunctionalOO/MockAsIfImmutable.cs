using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace FunctionalOO
{
    public class DataStructure
    {
        public int Value { get; set; }
        public DataStructure(int value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DataStructure);
        }

        protected bool Equals(DataStructure other)
        {
            return other != null && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }

    public class MockAsIfImmutable
    {
        public virtual DataStructure AddMutable(DataStructure input, int toAdd)
        {
            input.Value += toAdd;
            return input;
        }
    }

    public class ConsumeMutableCode
    {
        private readonly MockAsIfImmutable _dep;

        public ConsumeMutableCode(MockAsIfImmutable dep)
        {
            _dep = dep;
        }

        public int CalculateSum(params int[] values)
        {
            var obj = new DataStructure(0);
            obj = values.Aggregate(obj, (current, value) => _dep.AddMutable(current, value));
            return obj.Value;
        }
        public int CalculateSumMutable(params int[] values)
        {
            var obj = new DataStructure(0);
            foreach (var value in values)
            {
                _dep.AddMutable(obj, value);
            }
            return obj.Value;
        }
    }

    [TestFixture]
    public class TestMockAsIfMutable
    {
        [Test]
        public void TestCalculateSum_AsIfImmutable()
        {
            var mock = new Mock<MockAsIfImmutable>();
            mock.Setup(m => m.AddMutable(new DataStructure(0), 11)).Returns(new DataStructure(11));
            mock.Setup(m => m.AddMutable(new DataStructure(11), 10)).Returns(new DataStructure(101));
            mock.Setup(m => m.AddMutable(new DataStructure(101), 10)).Returns(new DataStructure(111));
            var subject = new ConsumeMutableCode(mock.Object);

            Assert.That(subject.CalculateSum(11, 10, 10), Is.EqualTo(111));
        }

        [Test]
        public void TestCalculateSum_AsIfMmutable()
        {
            var mock = new Mock<MockAsIfImmutable>();
            mock.Setup(m => m.AddMutable(It.Is((DataStructure ds) => ds.Value == 0), 11))
                .Callback((DataStructure ds, int v) => ds.Value = 11 );
            mock.Setup(m => m.AddMutable(It.Is((DataStructure ds) => ds.Value == 11), 10))
                .Callback((DataStructure ds, int v) => ds.Value = 101 );
            mock.Setup(m => m.AddMutable(It.Is((DataStructure ds) => ds.Value == 101), 10))
                .Callback((DataStructure ds, int v) => ds.Value = 111 );
            var subject = new ConsumeMutableCode(mock.Object);

            Assert.That(subject.CalculateSumMutable(11, 10, 10), Is.EqualTo(111));
        }
    }
}
