using Airport_Ticket_Booking.Enums;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Utilities;
using Airport_Ticket_Booking.Utilities.Extensions;

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
    }
}
