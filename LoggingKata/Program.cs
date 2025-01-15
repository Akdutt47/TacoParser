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
            logger.LogInfo("Log initialized");

            // Use File.ReadAllLines(path) to grab all the lines from your csv file.
            var lines = File.ReadAllLines(csvPath);

            // Log the first line to check if the file is being read properly
            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of TacoParser
            var parser = new TacoParser();

            // Use the Select LINQ method to parse every line in lines collection
            var locations = lines.Select(parser.Parse).Where(loc => loc != null).ToArray();

            // Initialize variables to store the two Taco Bells with the largest distance
            ITrackable locA = null;
            ITrackable locB = null;
            double maxDistance = 0;

            // NESTED LOOPS SECTION
            for (int i = 0; i < locations.Length; i++)
            {
                // First loop: Choose the first location (locA)
                var tacoBellA = locations[i];
                var corA = new GeoCoordinate(tacoBellA.Location.Latitude, tacoBellA.Location.Longitude);

                for (int j = i + 1; j < locations.Length; j++)
                {
                    // Second loop: Choose the second location (locB)
                    var tacoBellB = locations[j];
                    var corB = new GeoCoordinate(tacoBellB.Location.Latitude, tacoBellB.Location.Longitude);

                    // Get the distance between the two Taco Bells
                    double distance = corA.GetDistanceTo(corB);

                    // If the distance is larger than the current maximum, update the variables
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        locA = tacoBellA;
                        locB = tacoBellB;
                    }
                }
            }

            // After looping through all the Taco Bells, display the results
            logger.LogInfo($"The two Taco Bells that are the farthest apart are:");
            logger.LogInfo($"1. {locA.Name} located at ({locA.Location.Latitude}, {locA.Location.Longitude})");
            logger.LogInfo($"2. {locB.Name} located at ({locB.Location.Latitude}, {locB.Location.Longitude})");
            logger.LogInfo($"Distance between them: {maxDistance / 1000} kilometers");
        }
    }
}

