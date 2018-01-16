namespace Raindrop.UnitTesting
{
    using System;
    using System.Collections.Generic;

    using Autofac.Extras.Moq;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public abstract class UnitTestBase<TClassUnderTest> where TClassUnderTest : class
    {
        private AutoMock _mock;

        private Lazy<TClassUnderTest> _classUnderTest;
        protected TClassUnderTest ClassUnderTest => _classUnderTest.Value;

        private readonly Queue<Mock> _activeMocks = new Queue<Mock>();

        [OneTimeSetUp]
        protected virtual void OneTimeSetUp()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        protected virtual void SetUp()
        {
            _classUnderTest = new Lazy<TClassUnderTest>(TestClassConstructor);
        }

        [OneTimeTearDown]
        protected virtual void OneTimeTearDown()
        {
        }

        [TearDown]
        protected virtual void TearDown()
        {
        }

        protected virtual TClassUnderTest TestClassConstructor() =>
            _mock.Create<TClassUnderTest>();

        protected Mock<TMock> GetMock<TMock>() where TMock : class =>
            LogMockCreation(_mock.Mock<TMock>());

        private Mock<TMock> LogMockCreation<TMock>(Mock<TMock> mock) where TMock : class
        {
            _activeMocks.Enqueue(mock);
            return mock;
        }
    }
}
