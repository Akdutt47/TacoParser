using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            // Split the line into an array of values using ',' as a delimiter
            var cells = line.Split(',');

            // If the line doesn't have enough parts, return null
            if (cells.Length < 3)
            {
                logger.LogError("Invalid data - not enough values in line.");
                return null;
            }

            // Parse latitude (Index 0)
            if (!double.TryParse(cells[0], out double latitude))
            {
                logger.LogError($"Failed to parse latitude: {cells[0]}");
                return null;
            }

            // Parse longitude (Index 1)
            if (!double.TryParse(cells[1], out double longitude))
            {
                logger.LogError($"Failed to parse longitude: {cells[1]}");
                return null;
            }

            // Get the name of the Taco Bell (Index 2)
            string name = cells[2];

            // Create an instance of the Point struct and set values
            var location = new Point
            {
                Latitude = latitude,
                Longitude = longitude
            };

            // Create an instance of the TacoBell class
            var tacoBell = new TacoBell
            {
                Name = name,
                Location = location
            };

            // Return the populated TacoBell instance
            return tacoBell;
        }
    }
}