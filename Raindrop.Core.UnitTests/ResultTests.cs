namespace Raindrop.Core.UnitTests
{
    using NUnit.Framework;

    using Raindrop.UnitTesting;
    using Raindrop.Utility;
    using System;

    public class ResultTests
    {
        [Test]
        public void BindCallsFunctionOnSuccess()
        {
            var result = Result.Succeed();
            var called = false;
            result.Bind(() => { called = true; return Result.Succeed(); });

            Assert.IsTrue(called);
        }

        [Test]
        public void BindDoesNotCallFunctionOnFailure()
        {
            var result = Result.Fail(string.Empty);
            var called = false;
            result.Bind(() => { called = true; return Result.Succeed(); });

            Assert.IsFalse(called);
        }

        [Test]
        public void BindWithFuncReturnsSuccessIfNoException()
        {
            var result = Result.Succeed().Bind(() => 1);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public void BindWithFuncReturnsFailureOnException()
        {
            var result = Result.Succeed().Bind((Func<int>)(() => throw new Exception()));

            Assert.IsFalse(result.Success);
        }

        [Test]
        public void BindWithFuncReturnsExceptionOnException()
        {
            var result = Result.Succeed().Bind((Func<int>)(() => throw new ApplicationException()));

            Assert.That(result.Exception is ApplicationException);
        }
    }
}
