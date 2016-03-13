using System;
using System.Linq;
using NUnit.Framework;

namespace FunctionalOO
{
    public interface IResolver
    {
        T Get<T>(object metadata);
    }

    public interface IDelegateIOC
    {
        object ResolveObject(IResolver r);
    }


    public class DelegateIOC<TDelegate> : IDelegateIOC
    {
        public TDelegate Resolve(IResolver r)
        {
            return func_(r);
        }

        private readonly Func<IResolver, TDelegate> func_;
        public DelegateIOC(Func<IResolver, TDelegate> delg)
        {
            func_ = delg;
        }

        public object ResolveObject(IResolver r)
        {
            return Resolve(r);
        }
    }

    public class DelegateMaker
    {
        public static DelegateIOC<TDelegate> With<TDep1, TDelegate>(Func<TDep1, TDelegate> f, params object[] metadata)
        {
            return new DelegateIOC<TDelegate>(r => f(r.Get<TDep1>(metadata?.FirstOrDefault())));
        }
        public static DelegateIOC<TDelegate> With<TDep1, TDep2, TDelegate>(Func<TDep1, TDep2, TDelegate> f, params object[] metadata)
        {
            return new DelegateIOC<TDelegate>(r => f(r.Get<TDep1>(metadata?.ElementAtOrDefault(0)), r.Get<TDep2>(metadata?.ElementAtOrDefault(1))));
        }
        public static DelegateIOC<TDelegate> With<TDep1, TDep2, TDep3, TDelegate>(Func<TDep1, TDep2, TDep3, TDelegate> f, params object[] metadata)
        {
            return new DelegateIOC<TDelegate>(r => f(r.Get<TDep1>(metadata?.ElementAtOrDefault(0)), r.Get<TDep2>(metadata?.ElementAtOrDefault(1)), r.Get<TDep3>(metadata?.ElementAtOrDefault(2))));
        }
    }

    public delegate string PythagS(string x, string y);

    //[DI Attribute]
    public static class Funcs
    {
        public delegate double Pythag(double x, double y);
        public delegate double Add(double x, double y);
        public delegate double Mult(double x, double y);
        public delegate double Sqrt(double y);
        public delegate double ParseDouble(string x);
        public delegate string DoubleToString(double x);

        public static Add AddF => (x, y) => x + y;
        public static Mult MultF => (x, y) => x * y;

        public static Sqrt SqrtF => Math.Sqrt;

        public static ParseDouble ParseDoubleF => Double.Parse;

        public static DoubleToString DoubleToStringF => d => d.ToString();

        public static IDelegateIOC PythagF =>
            DelegateMaker.With((Add addF, Mult multF, Sqrt sqrtF) => 
                (Pythag)((x, y) => 
                    sqrtF(addF(multF(x, x), multF(y, y)))));

        public static IDelegateIOC PythagSF =>
            DelegateMaker.With((Pythag py, ParseDouble parse, DoubleToString tos) => 
                (PythagS)((x, y) => 
                    tos(py(parse(x), parse(y)))));
    }


    public class KnownResolver : IResolver
    {
        private readonly object[] resolvable_;

        public KnownResolver(object[] resolvable)
        {
            resolvable_ = resolvable;
        }

        public T Get<T>(object metadata)
        {
            return (T)resolvable_.FirstOrDefault(o => o.GetType() == typeof(T));
        }
    }

    public static class IOCExtensions
    {
        public static T ResolveAs<T>(this IDelegateIOC ioc, IResolver r)
        {
            return (T)ioc.ResolveObject(r);
        }
    }

    [TestFixture]
    public class TestFromFuncs
    {
        [Test]
        public void TestDelegateIoc()
        {
            Funcs.Add mAdd = (x, y) => x + y;
            Funcs.Mult mMult = (x, y) => x * y;
            Funcs.Sqrt mSqrt = Math.Sqrt;
            var resolver = new KnownResolver(new object[] { mAdd, mMult, mSqrt });
            var pythag = Funcs.PythagF.ResolveAs<Funcs.Pythag>(resolver);
            Assert.That(pythag(3, 4), Is.EqualTo(5.0));
        }
    }

}
