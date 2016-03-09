using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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
}