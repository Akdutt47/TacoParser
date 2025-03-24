using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldReturnNonNullObject()
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638,-84.677017,Taco Bell Acworth");

            //Assert
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("34.073638,-84.677017,Taco Bell Acworth", -84.677017)]
        [InlineData("33.635282,-86.684056,Taco Bell Birmingham", -86.684056)]
        [InlineData("30.402123,-86.605877,Taco Bell Destin", -86.605877)]
        public void ShouldParseLongitude(string line, double expected)
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var result = tacoParser.Parse(line);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expected, result.Location.Longitude, 5);
        }

        [Theory]
        [InlineData("34.073638,-84.677017,Taco Bell Acworth", 34.073638)]
        [InlineData("33.635282,-86.684056,Taco Bell Birmingham", 33.635282)]
        [InlineData("30.402123,-86.605877,Taco Bell Destin", 30.402123)]
        public void ShouldParseLatitude(string line, double expected)
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var result = tacoParser.Parse(line);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expected, result.Location.Latitude, 5);
        }
    }
}