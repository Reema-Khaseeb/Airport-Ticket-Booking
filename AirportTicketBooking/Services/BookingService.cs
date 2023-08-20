using Airport_Ticket_Booking.Enums;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Services;
using Airport_Ticket_Booking.Utilities;
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

        public void BookFlight(Booking newBooking, UserRole userRole)
        {
            RoleAuthorization.CheckPermission(userRole, UserRole.Passenger);

            double price = CalculatePrice(newBooking.FlightNumber, newBooking.SelectedClass);
            newBooking.Price = price;
            newBooking.BookingDate = DateTime.Now;
            newBooking.BookingId = GetNextBookingId();
            Bookings.Add(newBooking);
        }

        private double CalculatePrice(int flightNumber, TicketClass ticketClass)
        {
            Flight flight = SearchFlightByFlightNumber(flightNumber);
            double price = 0;
            switch (ticketClass)
            {
                case TicketClass.Economy:
                    price = flight.EconomyPrice;
                    break;
                case TicketClass.Business:
                    price = flight.BusinessPrice;
                    break;
                case TicketClass.FirstClass:
                    price = flight.FirstClassPrice;
                    break;
            }
            return price;
        }

        private int GetNextBookingId()
        {
            int maxId = Bookings.Count > 0 ? Bookings.Max(b => b.BookingId) : 0;
            return maxId + 1;
        }

        private static Flight SearchFlightByFlightNumber(int flightNumber)
        {//TODO: delete this method. use instead ==> SerchFlight(criteria{flightNumber})
            Flight foundFlight = ManagerActions.GetAllFlights()
                .FirstOrDefault(flight => flight.FlightNumber == flightNumber);

            return foundFlight == null ? throw new InvalidOperationException($"Flight with number {flightNumber} not found.") : foundFlight;
        }
    }
}
