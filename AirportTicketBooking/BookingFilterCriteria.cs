using AirportTicketBooking.Enums;

namespace AirportTicketBooking
{
    public class BookingFilterCriteria : FlightFilterCriteria
    {
        public string PassportNumber { get; set; }
        public int FlightNumber { get; set; }
        public TicketClass TicketClass { get; set; }
    }
}
