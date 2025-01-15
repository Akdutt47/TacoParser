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

            // Split the input line into an array of strings, using ',' as the delimiter
            var cells = line.Split(',');

            // If the array length is less than 3, log an error and return null
            if (cells.Length < 3)
            {
                logger.LogError("Invalid line format. Expected at least 3 comma-separated values.");
                return null; 
            }

            // Parse latitude from the first part of the array (cells[0])
            double latitude;
            if (!double.TryParse(cells[0].Trim(), out latitude))
            {
                logger.LogError($"Invalid latitude value: {cells[0]}");
                return null;
            }

            // Parse longitude from the second part of the array (cells[1])
            double longitude;
            if (!double.TryParse(cells[1].Trim(), out longitude))
            {
                logger.LogError($"Invalid longitude value: {cells[1]}");
                return null;
            }

            // Grab the name from the third part of the array (cells[2])
            string name = cells[2].Trim();

            // Create a new Point struct to represent the location
            var location = new Point(latitude, longitude);

            // Create a new TacoBell instance and set its properties
            var tacoBell = new TacoBell
            {
                Name = name,
                Location = location
            };

            // Log the successful parsing
            logger.LogInfo($"Successfully parsed Taco Bell: {name}, Latitude: {latitude}, Longitude: {longitude}");

            // Return the TacoBell object, which implements ITrackable
            return tacoBell;
        }
    }
}
