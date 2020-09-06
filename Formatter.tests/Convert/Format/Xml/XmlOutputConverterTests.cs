using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using FormatConverter.Convert;
using FormatConverter.Convert.Format.Xml;
using NUnit.Framework;

namespace Formatter.tests.Convert.Format.Xml
{
    [TestOf(nameof(XmlOutputConverter))]
    [TestFixture]
    public class XmlOutputConverterTests
    {
        private XmlOutputConverter _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new XmlOutputConverter();
        }

        [Test]
        public void PrettySerialize_WhenNullObject_ShouldSReturnEmptyXmlString()
        {
            // Act
            var result = _classUnderTest.PrettySerialize(null);

            // Assert
            result.Should().BeEquivalentTo(string.Empty);
        }

        [Test]
        public void PrettySerialize_WhenEmptyObject_ShouldSReturnEmptyXmlString()
        {
            // Act
            var result = _classUnderTest.PrettySerialize(new SerializableExpando());

            // Assert
            result.Should().BeEquivalentTo("<XmlSerializableExpando />");
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
            result.Should().BeEquivalentTo(
                "<XmlSerializableExpando>\r\n  <Name type=\"System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">Panos</Name>\r\n  <Surname type=\"System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">Anastasiadis</Surname>\r\n  <Address type=\"FormatConverter.Convert.XmlSerializableExpando, FormatConverter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\">\r\n    <line1 type=\"System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">90 Clarence House</line1>\r\n    <line2 type=\"System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\">The Boulevard</line2>\r\n  </Address>\r\n</XmlSerializableExpando>");
        }
    }
}
