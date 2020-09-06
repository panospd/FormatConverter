﻿using System;
using System.Text.Json;
using FormatConverter.Convert.Format.Json;
using NUnit.Framework;

namespace Formatter.tests
{
    [TestOf(nameof(JsonReader))]
    [TestFixture]
    public class JsonReaderTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void ParseFile_WhenFileIsInvalid_ShouldThrowFormatException(string content)
        {
            // Arrange
            var classUnderTest = CreateClassUnderTest(() => content);

            // Act
            // Assert
            Assert.Throws<FormatException>(() => classUnderTest.ParseFile(""), "Json file is empty");
        }

        [Test]
        public void ParseFile_WhenFileIsValid_ShouldConvertToExpando()
        {
            // Arrange
            var content = "{\r\n  \"Name\": \"Panos\",\r\n  \"Address\": {\r\n    \"line1\": \"90 Clarence House\",\r\n    \"line2\":  \"The Boulevard\" \r\n  } \r\n}";

            var classUnderTest = CreateClassUnderTest(() => content);

            // Act
            var result = classUnderTest.ParseFile("");

            // Assert
            Assert.AreEqual(result["Name"].ToString(), "Panos");

            var actualAddress = (JsonElement)result["Address"];
            Assert.AreEqual(actualAddress.GetProperty("line1").GetString(), "90 Clarence House");
            Assert.AreEqual(actualAddress.GetProperty("line2").GetString(), "The Boulevard");
        }

        private JsonReader CreateClassUnderTest(Func<string> mockContent)
        {
            return new JsonReaderTest(mockContent);
        }

        public class JsonReaderTest : JsonReader
        {
            private readonly Func<string> _mockContent;

            public JsonReaderTest(Func<string> mockContent)
            {
                _mockContent = mockContent;
            }

            protected override string ReadFromFile(string path)
            {
                return _mockContent.Invoke();
            }
        }
    }
}