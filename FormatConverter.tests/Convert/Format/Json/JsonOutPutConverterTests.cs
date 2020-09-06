using FluentAssertions;
using FormatConverter.Convert;
using FormatConverter.Convert.Format.Json;
using NUnit.Framework;

namespace Formatter.tests.Convert.Format.Json
{
    [TestOf(nameof(JsonOutPutConverter))]
    [TestFixture]
    public class JsonOutPutConverterTests
    {
        private JsonOutPutConverter _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new JsonOutPutConverter();
        }

        [Test]
        public void PrettySerialize_WhenEmptyObject_ShouldReturnEmptyJsonString()
        {
            // Act
            var result = _classUnderTest.PrettySerialize(new SerializableExpando());

            // Assert
            result.Should().BeEquivalentTo("{}");
        }

        [Test]
        public void PrettySerialize_WhenNull_ShouldSerializeInputIntended()
        {
            // Act
            var result = _classUnderTest.PrettySerialize(null);

            // Assert
            result.Should().BeEquivalentTo("{}");
        }

        [Test]
        public void PrettySerialize_WhenCalled_ShouldSerializeInputIntended()
        {
            // Arrange
            var address = new SerializableExpando
            {
                {"line1", "90 Clarence House"},
                {"line2", "The Boulevard"}
            };

            var output = new SerializableExpando
            {
                {"Name", "Panos"}, 
                {"Surname", "Anastasiadis"},
                {"Address", address}
            };

            // Act
            var result = _classUnderTest.PrettySerialize(output);

            // Assert
            result.Should()
                .BeEquivalentTo(
                    "{\r\n  \"Name\": \"Panos\",\r\n  \"Surname\": \"Anastasiadis\",\r\n  \"Address\": {\r\n    \"line1\": \"90 Clarence House\",\r\n    \"line2\": \"The Boulevard\"\r\n  }\r\n}");
        }
    }
}