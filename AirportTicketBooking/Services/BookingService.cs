using AirportTicketBooking.Enums;
using AirportTicketBooking.Interfaces;
using AirportTicketBooking.Models;
using AirportTicketBooking.Utilities;

namespace AirportTicketBooking.Services
{
    internal class BookingService: IBookingService
    {
        private static List<Booking> Bookings { get; set; }

        public BookingService(List<Booking>? newBooking = null)
        {
            Bookings = newBooking ?? new List<Booking>();
        }

        public static List<Booking> GetAllBookings() => Bookings;

        public void BookFlight(Booking newBooking, UserRole userRole)
        {
            RoleAuthorization.CheckPermission(userRole, UserRole.Passenger);

            var price = CalculatePrice(newBooking.FlightNumber, newBooking.SelectedClass);
            newBooking.Price = price;
            newBooking.BookingDate = DateTime.Now;
            newBooking.BookingId = GetNextBookingId();
            Bookings.Add(newBooking);
        }

        public void CancelBooking(int bookingId, UserRole userRole)
        {
            RoleAuthorization.CheckPermission(userRole, UserRole.Passenger);

            var bookingToRemove = Bookings.FirstOrDefault(booking => booking.BookingId == bookingId);

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
            var bookingToModify = GetBookingByBookingId(bookingId);

            if (bookingToModify != null)
            {
                var newPrice = CalculatePrice(bookingToModify.FlightNumber, newTicketClass);

                var oldTicketClass = bookingToModify.SelectedClass;
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

        public void ViewPassengerBookings(string passportNumber)
        {
            var passengerBookings = GetPassengerBookings(passportNumber);
            Console.WriteLine("----------------- ViewPassengerBookings ----------------- ");
            ConsoleUtility.PrintBookings(passengerBookings);
        }

        public List<Booking> GetPassengerBookings(string passportNumber)
        {
            var filteredBookings = Bookings.Where(booking => booking.PassportNumber == passportNumber).ToList();
            return filteredBookings;
        }

        public void ViewAllBookings() => ConsoleUtility.PrintBookings(Bookings);
                
        private Booking GetBookingByBookingId(int bookingId)
        {
            return Bookings.FirstOrDefault(booking => booking.BookingId == bookingId);
        }
        
        private double CalculatePrice(int flightNumber, TicketClass ticketClass)
        {
            var flight = SearchFlightByFlightNumber(flightNumber);
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
            var maxId = Bookings.Max(b => b?.BookingId) ?? 0;
            return maxId + 1;
        }

        private static Flight SearchFlightByFlightNumber(int flightNumber)
        {
            var foundFlight = ManagerActions.GetAllFlights()
                .FirstOrDefault(flight => flight.FlightNumber == flightNumber);

            return foundFlight == null ? throw new InvalidOperationException($"Flight with number {flightNumber} not found.") : foundFlight;
        }

    }
}
