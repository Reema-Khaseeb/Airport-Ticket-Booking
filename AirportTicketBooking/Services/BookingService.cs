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

        public void CancelBooking(int bookingId, UserRole userRole)
        {
            RoleAuthorization.CheckPermission(userRole, UserRole.Passenger);

            Booking bookingToRemove = Bookings.FirstOrDefault(booking => booking.BookingId == bookingId);

            if (bookingToRemove == null)
            {
                Console.WriteLine($"\nBooking with ID {bookingId} not found.\n");
            }
            else
            {
                Bookings.Remove(bookingToRemove);
                Console.WriteLine($"\nBooking {bookingId} has been canceled.\n\n");
            }
        }

        public void ModifyBooking(int bookingId, TicketClass newTicketClass, UserRole userRole)
        {
            RoleAuthorization.CheckPermission(userRole, UserRole.Passenger);
            Booking bookingToModify = GetBookingByBookingId(bookingId);

            if (bookingToModify != null)
            {
                double newPrice = CalculatePrice(bookingToModify.FlightNumber, newTicketClass);

                TicketClass oldTicketClass = bookingToModify.SelectedClass;
                bookingToModify.SelectedClass = newTicketClass;
                bookingToModify.Price = newPrice;

                Console.WriteLine($"\nBooking {bookingId} Ticket Class modified successfully from " +
                                  $"'{oldTicketClass}' Class into '{newTicketClass}' Class.\n\n");
            }
            else
            {
                Console.WriteLine($"Booking with ID {bookingId} not found.\n\n");
            }
        }

        private Booking GetBookingByBookingId(int bookingId)
        {
            return Bookings.FirstOrDefault(booking => booking.BookingId == bookingId);
        }

        public void ViewPassengerBookings(string passportNumber)
        {
            List<Booking> passengerBookings = GetPassengerBookings(passportNumber);

            if (passengerBookings == null || passengerBookings.Count == 0)
            {
                Console.WriteLine("No bookings available.");
                return;
            }

            Console.WriteLine("----------------- ViewPassengerBookings ----------------- ");
            foreach (var booking in passengerBookings)
            {
                Console.WriteLine($"Booking ID: {booking.BookingId}");
                Console.WriteLine($"Flight Number: {booking.FlightNumber}");
                Console.WriteLine($"Passport Number: {booking.PassportNumber}");
                Console.WriteLine($"Price: {booking.Price}");
                Console.WriteLine($"Booking Date: {booking.BookingDate}");
                Console.WriteLine($"Selected Class: {booking.SelectedClass}");
                Console.WriteLine();
            }
        }

        public List<Booking> GetPassengerBookings(string passportNumber)
        {
            List<Booking> filteredBookings = Bookings.Where(booking => booking.PassportNumber == passportNumber).ToList();
            return filteredBookings;
        }

        public void PrintAllBookings()
        {
            if (Bookings == null || Bookings.Count == 0)
            {
                Console.WriteLine("No bookings available.");
                return;
            }

            foreach (var booking in Bookings)
            {
                Console.WriteLine($"Booking ID: {booking.BookingId}");
                Console.WriteLine($"Flight Number: {booking.FlightNumber}");
                Console.WriteLine($"Passport Number: {booking.PassportNumber}");
                Console.WriteLine($"Price: {booking.Price}");
                Console.WriteLine($"Booking Date: {booking.BookingDate}");
                Console.WriteLine($"Selected Class: {booking.SelectedClass}");
                Console.WriteLine();
            }
        }
    }
}
