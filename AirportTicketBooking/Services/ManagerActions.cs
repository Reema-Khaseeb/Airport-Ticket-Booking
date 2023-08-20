using Airport_Ticket_Booking.Enums;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Utilities;

namespace Airport_Ticket_Booking.Services
{
    internal class ManagerActions
    {
        private static List<Flight> flights;
        //private List<Flight> Flights { get; } = new List<Flight>();

        public ManagerActions(List<Flight>? initialFlights = null)
        {
            flights = initialFlights ?? new List<Flight>();
        }

        public void AddFlights(List<Flight> newFlights, UserRole userRole)
        {
            RoleAuthorization.CheckPermission(userRole, UserRole.Manager);
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
