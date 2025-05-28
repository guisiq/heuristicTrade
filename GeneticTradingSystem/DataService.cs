using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GeneticTradingSystem
{
    /// <summary>
    /// Provides services for loading financial data.
    /// </summary>
    public static class DataService
    {
        /// <summary>
        /// Loads OHLC (Open, High, Low, Close) data from a CSV file.
        /// </summary>
        /// <param name="filePath">The path to the CSV file.</param>
        /// <param name="hasHeader">Indicates whether the CSV file has a header row. Defaults to true.</param>
        /// <returns>A list of OHLCDataPoint objects loaded from the CSV file.</returns>
        /// <remarks>
        /// The CSV file is expected to have columns in the order: Date,Open,High,Low,Close,Volume.
        /// Rows with parsing errors will be skipped, and a warning will be printed to the console.
        /// </remarks>
        public static List<OHLCDataPoint> LoadDataFromCsv(string filePath, bool hasHeader = true)
        {
            var dataPoints = new List<OHLCDataPoint>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Warning: File not found at path: {filePath}");
                return dataPoints; // Return empty list if file does not exist
            }

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    if (hasHeader && !reader.EndOfStream)
                    {
                        reader.ReadLine(); // Skip header row
                    }

                    string line;
                    int lineNumber = hasHeader ? 1 : 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNumber++;
                        var values = line.Split(',');

                        if (values.Length < 6)
                        {
                            Console.WriteLine($"Warning: Skipping row {lineNumber}. Insufficient columns. Expected 6, got {values.Length}. Line: \"{line}\"");
                            continue;
                        }

                        try
                        {
                            DateTime date = DateTime.Parse(values[0], CultureInfo.InvariantCulture);
                            double open = double.Parse(values[1], CultureInfo.InvariantCulture);
                            double high = double.Parse(values[2], CultureInfo.InvariantCulture);
                            double low = double.Parse(values[3], CultureInfo.InvariantCulture);
                            double close = double.Parse(values[4], CultureInfo.InvariantCulture);
                            double volume = double.Parse(values[5], CultureInfo.InvariantCulture);

                            dataPoints.Add(new OHLCDataPoint(date, open, high, low, close, volume));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Warning: Skipping row {lineNumber} due to parsing error: {ex.Message}. Line: \"{line}\"");
                        }
                        catch (Exception ex) // Catch any other unexpected errors for a row
                        {
                            Console.WriteLine($"Warning: Skipping row {lineNumber} due to unexpected error: {ex.Message}. Line: \"{line}\"");
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                // Depending on requirements, might re-throw or handle differently
            }
            catch (Exception ex) // Catch other unexpected errors during file processing
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                // Depending on requirements, might re-throw or handle differently
            }

            return dataPoints;
        }
    }
}
