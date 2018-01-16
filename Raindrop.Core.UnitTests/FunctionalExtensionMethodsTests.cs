namespace Raindrop.Core.UnitTests
{
    using NUnit.Framework;
    using Raindrop.Utility;

    [TestFixture]
    public class FunctionalExtensionMethodsTests
    {
        [Test]
        [TestCase(3, ExpectedResult = 3)]
        [TestCase("Test", ExpectedResult = "Test")]
        [TestCase(null, ExpectedResult = null)]
        public object MapPassesExpectedValue(object value)
        {
            object passedValue = null;
            value.Map(x => passedValue = x);
            return passedValue;
        }

        [Test]
        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(1, 2, ExpectedResult = false)]
        [TestCase("Test", "Test", ExpectedResult = true)]
        public bool MapReturnsExpectedValue(object item1, object item2) =>
            item1.Map(item2.Equals);

        [Test]
        [TestCase(3, ExpectedResult = 3)]
        [TestCase("Test", ExpectedResult = "Test")]
        [TestCase(null, ExpectedResult = null)]
        public object TeePassesExpectedValue(object value)
        {
            object passedValue = null;
            value.Tee(x => passedValue = x);
            return passedValue;
        }

        [Test]
        public void TeeReturnsExpectedValue()
        {
            var result = 3.Tee(x => x = x + 1);
            Assert.AreEqual(result, 3);
        }
    }
}
