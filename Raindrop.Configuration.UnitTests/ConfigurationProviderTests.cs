namespace Raindrop.Configuration.UnitTests
{
    using System;
    using System.IO;
    using System.Text;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using Raindrop.UnitTesting;

    [TestFixture]
    public class ConfigurationProviderTests : UnitTestBase<ConfigurationProvider>
    {
        [Test]
        public void GetConfigurationReturnsCorrectConfigurationWithAttribute()
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(TestConfigurationWithAttributeJson)))
            {
                GetMock<IFileStreamProvider>()
                    .Setup(m => m.OpenFileForRead(It.IsAny<string>()))
                    .Returns(stream);

                var result = ClassUnderTest.GetConfiguration<TestConfigurationWithAttribute>();

                result.Should().Be(TestConfigurationWithAttributeItem);
            }
        }

        [Test]
        public void GetConfigurationReturnsCorrectConfigurationWithoutAttribute()
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(TestConfigurationWithoutAttributeJson)))
            {
                GetMock<IFileStreamProvider>()
                    .Setup(m => m.OpenFileForRead(It.IsAny<string>()))
                    .Returns(stream);

                var result = ClassUnderTest.GetConfiguration<TestConfigurationWithoutAttribute>();

                result.Should().Be(TestConfigurationWithoutAttributeItem);
            }
        }

        private static readonly TestConfigurationWithAttribute TestConfigurationWithAttributeItem = new TestConfigurationWithAttribute
        {
            StringItem = "This is a test",
            NumberItem = 12,
            SubConfigurationItem = new TestSubConfiguration
            {
                StringItem = "This is another test",
                NumberItem = 34
            }
        };

        private const string TestConfigurationWithAttributeJson = @"
{ ""TestConfiguration"": {
    ""StringItem"": ""This is a test"",
    ""NumberItem"": 12,
    ""SubConfigurationItem"": {
        ""StringItem"": ""This is another test"",
        ""NumberItem"": 34
    }
}}";

        private static readonly TestConfigurationWithoutAttribute TestConfigurationWithoutAttributeItem = new TestConfigurationWithoutAttribute
        {
            StringItem = "This is a test",
            NumberItem = 12,
            SubConfigurationItem = new TestSubConfiguration
            {
                StringItem = "This is another test",
                NumberItem = 34
            }
        };

        private const string TestConfigurationWithoutAttributeJson = @"
{ ""Raindrop.Configuration.UnitTests.ConfigurationProviderTests+TestConfigurationWithoutAttribute"": {
    ""StringItem"": ""This is a test"",
    ""NumberItem"": 12,
    ""SubConfigurationItem"": {
        ""StringItem"": ""This is another test"",
        ""NumberItem"": 34
    }
}}";

        private abstract class BaseTestConfiguration : IEquatable<BaseTestConfiguration>
        {
            public string StringItem { get; set; }
            public int NumberItem { get; set; }
            public TestSubConfiguration SubConfigurationItem { get; set; }

            public bool Equals(BaseTestConfiguration other) =>
                StringItem.Equals(other.StringItem)
                && NumberItem.Equals(other.NumberItem)
                && SubConfigurationItem.Equals(other.SubConfigurationItem);

            public override bool Equals(object obj) =>
                (obj as BaseTestConfiguration)?.Equals(this) ?? false;
        }

        [ConfigurationName("TestConfiguration")]
        private class TestConfigurationWithAttribute : BaseTestConfiguration
        {
        }

        private class TestConfigurationWithoutAttribute : BaseTestConfiguration
        {
        }

        private class TestSubConfiguration : IEquatable<TestSubConfiguration>
        {
            public string StringItem { get; set; }
            public int NumberItem { get; set; }

            public bool Equals(TestSubConfiguration other) =>
                StringItem.Equals(other.StringItem)
                && NumberItem.Equals(other.NumberItem);

            public override bool Equals(object obj) =>
                (obj as TestSubConfiguration)?.Equals(this) ?? false;
        }
    }
}
