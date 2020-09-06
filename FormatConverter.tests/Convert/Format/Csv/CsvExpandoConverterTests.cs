using System.Collections.Generic;
using FluentAssertions;
using FormatConverter.Convert;
using FormatConverter.Convert.Format.Csv;
using NUnit.Framework;

namespace Formatter.tests.Convert.Format.Csv
{
    [TestOf(nameof(CsvExpandoConverter))]
    [TestFixture]
    public class CsvExpandoConverterTests
    {
        private CsvExpandoConverter _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new CsvExpandoConverter();
        }

        [Test]
        public void ToExpando_WhenValidContent_ShouldConvertToExpando()
        {
            // Arrange
            var content = new List<List<string>>
            {
                new List<string>
                {
                    "Name",
                    "Surname",
                    "Address_line1",
                    "Address_line2"
                },
                new List<string>
                {
                    "Panos",
                    "Anastasiadis",
                    "90 Clarence House",
                    "The Boulevard"
                }
            };

            // Act
            var result = _classUnderTest.ToExpando(content);

            var address = new SerializableExpando();
            address.Add("line1", "90 Clarence House");
            address.Add("line2", "The Boulevard");

            var expected = new SerializableExpando();
            expected.Add("Name", "Panos");
            expected.Add("Surname", "Anastasiadis");
            expected.Add("Address", address);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
