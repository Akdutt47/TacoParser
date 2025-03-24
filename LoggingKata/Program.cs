using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // Objective: Find the two Taco Bells that are the farthest apart from one another.

            logger.LogInfo("Log initialized");

            // Read all lines from the CSV file
            var lines = File.ReadAllLines(csvPath);

            // Log an error if file is empty, or a warning if it only has one line
            if (lines.Length == 0)
            {
                logger.LogError("File has no data!");
                return;
            }
            else if (lines.Length == 1)
            {
                logger.LogWarning("File contains only one location.");
            }

            logger.LogInfo($"Lines read: {lines.Length}");

            // Create an instance of TacoParser and parse the locations
            var parser = new TacoParser();
            var locations = lines.Select(parser.Parse).ToArray();

            // Variables to store the farthest apart locations
            ITrackable locA = null;
            ITrackable locB = null;
            double maxDistance = 0;

            // Loop through each location to compare distances
            for (int i = 0; i < locations.Length; i++)
            {
                var origin = locations[i];

                // Create a coordinate object for origin
                var corA = new GeoCoordinate(origin.Location.Latitude, origin.Location.Longitude);

                for (int j = i + 1; j < locations.Length; j++)
                {
                    var destination = locations[j];

                    // Create a coordinate object for destination
                    var corB = new GeoCoordinate(destination.Location.Latitude, destination.Location.Longitude);

                    // Calculate the distance between the two locations
                    double distance = corA.GetDistanceTo(corB);

                    // If the calculated distance is greater than maxDistance, update variables
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        locA = origin;
                        locB = destination;
                    }
                }
            }

            // Display the results
            if (locA != null && locB != null)
            {
                logger.LogInfo($"The two farthest Taco Bells are:\n" +
                               $"{locA.Name} and {locB.Name}.\n" +
                               $"They are {maxDistance / 1609.34:F2} miles apart.");
            }
        }
    }
}
