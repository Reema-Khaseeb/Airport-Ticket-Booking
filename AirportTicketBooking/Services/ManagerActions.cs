using Microsoft.Extensions.Configuration;
using System.IO;
using Airport_Ticket_Booking.Enums;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Utilities;
using AirportTicketBooking.Utilities.DataHandler;
using System.Configuration;


namespace Airport_Ticket_Booking.Services
{
    internal class ManagerActions
    {
        private static List<Flight> flights;
        //private List<Flight> Flights { get; } = new List<Flight>();
        private readonly string _systemFlightsDataFilePath;

        private readonly IConfiguration _configuration;
        public ManagerActions(IConfiguration configuration, string filePath)
        {
            _configuration = configuration;
            _systemFlightsDataFilePath = _configuration["SystemFlightsDataFilePath"];
            if (string.IsNullOrEmpty(filePath))
            {
                flights = new List<Flight>();
            }
            else
            {
                flights = FileProcessor.ReadFlightsFromCsv(filePath);
                FileProcessor.WriteFlightsToCsv(_systemFlightsDataFilePath, flights);
            }
        }

        public void AddFlights(string filePath, UserRole userRole)
        {
            RoleAuthorization.CheckPermission(userRole, UserRole.Manager);

            var newFlights = FileProcessor.ReadFlightsFromCsv(filePath);
            FileProcessor.WriteFlightsToCsv(_systemFlightsDataFilePath, newFlights);
            flights.AddRange(newFlights);
        }

        public void ViewFlights()
        {
            ViewFlightConsoleStyler.PrintFlightHeader();
            ViewFlightConsoleStyler.PrintHorizontalLine();
            foreach (var flight in flights)
            {
                ViewFlightConsoleStyler.PrintFlightRow(flight);
                ViewFlightConsoleStyler.PrintHorizontalLine();
            }
        }

        public static List<Flight> GetAllFlights() => flights;
    }
}
