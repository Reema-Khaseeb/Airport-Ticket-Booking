using Airport_Ticket_Booking3.Models;

namespace AirportTicketBooking.Services
{
    internal class BookingService
    {
        //public List<Booking> Bookings { get; private set; }
        private static List<Booking> Bookings { get; set; }

        public BookingService(List<Booking>? newBooking = null)
        {
            Bookings = newBooking ?? new List<Booking>();
        }

        public static List<Booking> GetAllBookings() => Bookings;
    }
}
