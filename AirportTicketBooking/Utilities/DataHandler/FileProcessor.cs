using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Airport_Ticket_Booking.Models;


namespace AirportTicketBooking.Utilities.DataHandler
{
    internal class FileProcessor
    {
        public static List<Flight> ReadFlightsFromCsv(string csvFilePath)
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<FlightMap>();
                return csv.GetRecords<Flight>().ToList();
            }
        }
        
         public static List<string> WriteFlightsToCsv(string csvFilePath, List<Flight> flights) 
         {
            var errors = new List<string>();

            try
            {
                var fileExists = File.Exists(csvFilePath);

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = !fileExists // Set HasHeaderRecord based on whether the file exists
                };

                using (var writer = new StreamWriter(csvFilePath, append: fileExists)) // Use append mode if file exists
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.Context.RegisterClassMap<FlightMap>();
                    
                    foreach (var flight in flights)
                    {
                        var validationResults = new List<ValidationResult>();
                        var context = new ValidationContext(flight);
                        if (!Validator.TryValidateObject(flight, context, validationResults, validateAllProperties: true))
                        {
                            foreach (var validationResult in validationResults)
                            {
                                errors.Add(validationResult.ErrorMessage);
                            }
                        }
                    }

                    if (errors.Count == 0)
                    {
                        csv.WriteRecords(flights);
                    }
                }
                if (errors.Count == 0)
                {
                    Console.WriteLine("Data appended successfully.");
                }
                else
                {
                    Console.WriteLine("Errors encountered while appending data:");
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error);
                    }
                }
            }
            catch (Exception ex)
            {
                errors.Add($"An error occurred: {ex.Message}");
            }

            return errors;
         }
    }
}
