using Airport_Ticket_Booking.Enums;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Utilities;
using AirportTicketBooking.Utilities.DataHandler;


namespace Airport_Ticket_Booking.Services
{
    internal class ManagerActions
    {
        private static List<Flight> flights;
        //private List<Flight> Flights { get; } = new List<Flight>();
        
        public ManagerActions(string filePath)
        {
            var systemFlightsDataFilePath =
                @"C:\\Users\\DELL\\Documents\\GitHub\\Airport-Ticket-Booking\\AirportTicketBooking\\flights.csv";

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

        public void AddFlights(string filePath, UserRole userRole)
        {

            RoleAuthorization.CheckPermission(userRole, UserRole.Manager);
            
            var systemFlightsDataFilePath =
                @"C:\\Users\\DELL\\Documents\\GitHub\\Airport-Ticket-Booking\\AirportTicketBooking\\flights.csv";
            var newFlights = FileProcessor.ReadFlightsFromCsv(filePath);
            
            //FileProcessor.WriteFlightsToCsv(_systemFlightsDataFilePath, newFlights);
            FileProcessor.WriteFlightsToCsv(systemFlightsDataFilePath, newFlights);
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
