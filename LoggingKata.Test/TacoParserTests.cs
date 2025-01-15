using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldReturnNonNullObject()
        {
            // Arrange
            var tacoParser = new TacoParser();

            // Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            // Assert
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]  // Example inline data
        public void ShouldParseLongitude(string line, double expected)
        {
            // Arrange
            var tacoParser = new TacoParser();

            // Act
            var actual = tacoParser.Parse(line);
            var actualLongitude = actual.Longitude;  // Assuming Parse returns an object with a Longitude property

            // Assert
            Assert.Equal(expected, actualLongitude);
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]  // Example inline data
        public void ShouldParseLatitude(string line, double expected)
        {
            // Arrange
            var tacoParser = new TacoParser();

            // Act
            var actual = tacoParser.Parse(line);
            var actualLatitude = actual.Latitude;  // Assuming Parse returns an object with a Latitude property

            // Assert
            Assert.Equal(expected, actualLatitude);
        }
    }

    // Assuming this is the class definition for TacoParser
    public class TacoParser
    {
        public Location Parse(string line)
        {
            // Split the input string by commas and extract the latitude and longitude
            var parts = line.Split(',');
            double latitude = double.Parse(parts[0].Trim());
            double longitude = double.Parse(parts[1].Trim());

            return new Location
            {
                Latitude = latitude,
                Longitude = longitude
            };
        }
    }

    // Assuming this is the Location class definition
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

