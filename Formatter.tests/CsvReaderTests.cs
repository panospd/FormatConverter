using System;
using System.Collections.Generic;
using FluentAssertions;
using FormatConverter.Convert;
using FormatConverter.Convert.Format.Csv;
using NUnit.Framework;

namespace Formatter.tests
{
    [TestOf(nameof(CsvReader))]
    [TestFixture]
    public class CsvReaderTests
    {
        [Test]
        public void ReadFromFile_WhenInvalidInput_ShouldThrowFormatException()
        {
            // Arrange
            var classUnderTest = CreateClassUnderTest(() => 
                new List<List< string >>
                {
                    new List<string>
                    {
                        "Name",
                        "Surname"
                    },
                    new List<string>
                    {
                        "Panos"
                    }
                });

            // Act
            // Assert
            Assert.Throws<FormatException>(() => classUnderTest.ParseFile(""), "CSV file is either empty or not in a correct format");
        }

        [Test]
        public void ReadFromFile_WhenFileContainsMoreThanTwoLines_ShouldThrowFormatException()
        {
            // Arrange
            var classUnderTest = CreateClassUnderTest(() =>
                new List<List<string>>
                {
                    new List<string>
                    {
                        "Name",
                        "Surname"
                    },
                    new List<string>
                    {
                        "Panos",
                        "Anastasiadis"
                    },
                    new List<string>()
                });

            // Act
            // Assert
            Assert.Throws<FormatException>(() => classUnderTest.ParseFile(""), "CSV file is either empty or not in a correct format");
        }

        [Test]
        public void ReadFromFile_WhenFileContainsLessThanTwoLines_ShouldThrowFormatException()
        {
            // Arrange
            var classUnderTest = CreateClassUnderTest(() =>
                new List<List<string>>
                {
                    new List<string>
                    {
                        "Name",
                        "Surname"
                    },
                });

            // Act
            // Assert
            Assert.Throws<FormatException>(() => classUnderTest.ParseFile(""), "CSV file is either empty or not in a correct format");
        }

        [Test]
        public void ReadFromFile_WhenFileIsEmpty_ShouldThrowFormatException()
        {
            // Arrange
            var classUnderTest = CreateClassUnderTest(() => new List<List<string>>());

            // Act
            // Assert
            Assert.Throws<FormatException>(() => classUnderTest.ParseFile(""), "CSV file is either empty or not in a correct format");
        }

        [Test]
        public void ReadFromFile_WhenValidInput_ShouldConvertToExpando()
        {
            // Arrange
            var classUnderTest = CreateClassUnderTest(() =>
                new List<List<string>>
                {
                    new List<string>
                    {
                        "Name",
                        "Surname"
                    },
                    new List<string>
                    {
                        "Panos",
                        "Anastasiadis"
                    }
                });

            // Act
            var result = classUnderTest.ParseFile("");

            // Assert
            var expected = new SerializableExpando();
            expected.Add("Name", "Panos");
            expected.Add("Surname", "Anastasiadis");

            result.Should().BeEquivalentTo(expected);
        }

        private static CsvReader CreateClassUnderTest(Func<List<List<string>>> mockContent)
        {
            return new CsvReaderTest(mockContent);
        }

        public class CsvReaderTest : CsvReader
        {
            private readonly Func<List<List<string>>> _mockContent;

            public CsvReaderTest(Func<List<List<string>>> mockContent)
            {
                _mockContent = mockContent;
            }

            protected override List<List<string>> ReadFromFile(string path)
            {
                return _mockContent.Invoke();
            }
        }
    }
}