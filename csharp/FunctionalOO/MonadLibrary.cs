using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FunctionalOO
{
    public class EitherEx<TSuccess>
    {
        public bool IsSuccess { get; }
        private readonly TSuccess _successValue;
        private readonly Exception _failureValue;

        public TSuccess SuccessValue
        {
            get
            {
                if (!IsSuccess) throw _failureValue;
                return _successValue;
            }
        }

        public Exception FailureValue
        {
            get
            {
                if (IsSuccess) throw new Exception("mismatch");
                return _failureValue;
            }
        }

        public EitherEx(TSuccess success)
        {
            IsSuccess = true;
            _successValue = success;
        }

        public EitherEx(Exception failure)
        {
            IsSuccess = false;
            _failureValue = failure;
        }

        public static implicit operator EitherEx<TSuccess>(TSuccess value)
        {
            return new EitherEx<TSuccess>(value);
        }

        public static implicit operator EitherEx<TSuccess>(Exception value)
        {
            return new EitherEx<TSuccess>(value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as EitherEx<TSuccess>);
        }

        protected bool Equals(EitherEx<TSuccess> other)
        {
            return IsSuccess == other.IsSuccess && EqualityComparer<TSuccess>.Default.Equals(_successValue, other._successValue) && Equals(_failureValue, other._failureValue);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = IsSuccess.GetHashCode();
                hashCode = (hashCode*397) ^ EqualityComparer<TSuccess>.Default.GetHashCode(_successValue);
                hashCode = (hashCode*397) ^ (_failureValue?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return IsSuccess
                ? $"{{Success: {_successValue}}}"
                : $"{{Failure: {_failureValue}}}";
        }
    }

    public static class EitherEx
    {
        public static EitherEx<TSuccess> Success<TSuccess>(TSuccess value) => new EitherEx<TSuccess>(value);  
        public static EitherEx<TSuccess> Failure<TSuccess>(Exception value) => new EitherEx<TSuccess>(value);  
    }

    public static class EitherExExtensions
    {
        public delegate EitherEx<TOut> EitherExBindFunc<in TIn, TOut>(TIn input);

        public static EitherEx<TOut> AndThen<TIn, TOut>(this EitherEx<TIn> m, EitherExBindFunc<TIn, TOut> f)
        {
            return m.IsSuccess ? f(m.SuccessValue) : EitherEx.Failure<TOut>(m.FailureValue);
        }
    }

    public class MonadLonghand
    {
        public static string DoSomeMath(double input)
        {
            return EitherEx.Success(input)
                .AndThen(SucceedIfOverFive)
                .AndThen(SucceedIfEven)
                .SuccessValue;
        }

        private static EitherEx<int> SucceedIfOverFive(double value)
        {
            if (value > 5.0)
            {
                return (int) (value - 5.0);
            }
            else
            {
                return new Exception("too low");
            }
        }

        private static EitherEx<string> SucceedIfEven(int value)
        {
            if (value%2 == 0)
            {
                return (value/2).ToString();
            }
            else
            {
                return new Exception("uneven");
            }
            
        }
    }

    [TestFixture]
    public class TestMonadLonghand
    {
        [Test]
        public void TestEitherEx()
        {
            Assert.That(MonadLonghand.DoSomeMath(13.2), Is.EqualTo("4"));
            Assert.That(() => MonadLonghand.DoSomeMath(3.2), Throws.Exception);
            Assert.That(() => MonadLonghand.DoSomeMath(12.2), Throws.Exception);
        }
    }
}
