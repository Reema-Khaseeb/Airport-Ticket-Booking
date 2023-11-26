using AirportTicketBooking.Enums;
using AirportTicketBooking.Interfaces;
using AirportTicketBooking.Models;
using AirportTicketBooking.Utilities;
using AirportTicketBooking.Utilities.DataHandler;


namespace AirportTicketBooking.Services
{
    internal class FlightService : IFlightService
    {
        private const string systemFlightsDataFilePath =
           @"C:\\Users\\DELL\\Documents\\GitHub\\Airport-Ticket-Booking\\AirportTicketBooking\\flights.csv";
        private static List<Flight> flights;

        private static FlightService instance;

        public FlightService(string filePath = null)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                flights = new List<Flight>();
            }
            else
            {
                flights = FileProcessor.ReadFlightsFromCsv(filePath);
                FileProcessor.WriteFlightsToCsv(systemFlightsDataFilePath, flights);
            }
        }

        public static FlightService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FlightService();
                }
                return instance;
            }
        }

        public void AddFlights(string filePath, UserRole userRole)
        {
            RoleAuthorization.CheckPermission(userRole, UserRole.Manager);
            var newFlights = FileProcessor.ReadFlightsFromCsv(filePath);            
            FileProcessor.WriteFlightsToCsv(systemFlightsDataFilePath, newFlights);
            flights.AddRange(newFlights);
        }

        public void ViewFlights()
        {
            ViewFlightConsoleStyler.ViewFlights(flights);
        }

        public static List<Flight> GetAllFlights() => flights;
                
        public void ViewSearchFlightsResults(List<Flight> searchResults)
        {
            if (!searchResults.Any())
            {
                Console.WriteLine("No flights found matching the search criteria.");
                return;
            }

            ViewFlightConsoleStyler.ViewFlights(searchResults);
        }

        public void SearchFlights(FlightFilterCriteria criteria)
        {
            var query = FilterFlights.SearchFlights(criteria);
            ViewSearchFlightsResults(query.ToList());
        }
    }
}
