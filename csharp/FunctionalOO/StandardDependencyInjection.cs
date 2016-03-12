using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace FunctionalOO
{
    public interface IDepdencency
    {
        Task<string> GoGetSomeValue(IPAddress addr, string route);
    }

    public class StandardDependencyInjection
    {
        private readonly IDepdencency _dependency;

        public StandardDependencyInjection(IDepdencency dependency)
        {
            _dependency = dependency;
        }

        public string UseDependency(string url)
        {
            return _dependency.GoGetSomeValue(IPAddress.Loopback, url).Result;
        }
    }

    [TestFixture]
    public class MockDependency
    {
        [Test]
        public void Test_UseDependency()
        {
            var dep = new Mock<IDepdencency>();
            var subject = new StandardDependencyInjection(dep.Object);
            dep.Setup(d => d.GoGetSomeValue(IPAddress.Loopback, "/foo/bar")).ReturnsAsync("it worked");
            Assert.That(subject.UseDependency("/foo/bar"), Is.EqualTo("it worked"));
        }
    }
}