using AirportTicketBooking.Enums;
using AirportTicketBooking.Models;
using AirportTicketBooking.Utilities;

namespace AirportTicketBooking.Services
{
    internal class BookingService
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
            var maxId = Bookings.Count > 0 ? Bookings.Max(b => b.BookingId) : 0;
            return maxId + 1;
        }

        private static Flight SearchFlightByFlightNumber(int flightNumber)
        {
            var foundFlight = ManagerActions.GetAllFlights()
                .FirstOrDefault(flight => flight.FlightNumber == flightNumber);

            return foundFlight == null ? throw new InvalidOperationException($"Flight with number {flightNumber} not found.") : foundFlight;
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

        private Booking GetBookingByBookingId(int bookingId)
        {
            return Bookings.FirstOrDefault(booking => booking.BookingId == bookingId);
        }

        public void ViewPassengerBookings(string passportNumber)
        {
            var passengerBookings = GetPassengerBookings(passportNumber);

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
            var filteredBookings = Bookings.Where(booking => booking.PassportNumber == passportNumber).ToList();
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

        public void ViewSearchFlightsResults(List<Flight> searchResults)
        {
            if (searchResults == null || searchResults.Count == 0)
            {
                Console.WriteLine("No flights found matching the search criteria.");
                return;
            }

            foreach (var flight in searchResults)
            {
                Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                Console.WriteLine($"Departure Country: {flight.DepartureCountry}");
                Console.WriteLine($"Destination Country: {flight.DestinationCountry}");
                Console.WriteLine($"Departure Date: {flight.DepartureDate}");
                Console.WriteLine($"Departure Airport: {flight.DepartureAirport}");
                Console.WriteLine($"Arrival Airport: {flight.ArrivalAirport}");
                Console.WriteLine($"Economy Price: {flight.EconomyPrice}");
                Console.WriteLine($"Business Price: {flight.BusinessPrice}");
                Console.WriteLine($"First Class Price: {flight.FirstClassPrice}");
                Console.WriteLine();
            }
        }

        public void SearchFlights(FlightFilterCriteria criteria)
        {
            Console.WriteLine("----------------- PrintSearchResults ----------------- ");
            var flights = ManagerActions.GetAllFlights();

            var query = flights.AsQueryable();

            query = FilterFlightByPriceRange(query, criteria.SpecificPrice, criteria.PriceRangeMin, criteria.PriceRangeMax);
            query = FilterFlightByDateRange(query, criteria.DepartureDate, 
                criteria.DepartureDateRangeMin, criteria.DepartureDateRangeMax);
            query = FilterFlightByString(query, f => f.DepartureCountry, criteria.DepartureCountry);
            query = FilterFlightByString(query, f => f.DestinationCountry, criteria.DestinationCountry);
            query = FilterFlightByString(query, f => f.DepartureAirport, criteria.DepartureAirport);
            query = FilterFlightByString(query, f => f.ArrivalAirport, criteria.ArrivalAirport);

            ViewSearchFlightsResults(query.ToList());
        }

        private IQueryable<Flight> FilterFlightByPriceRange(IQueryable<Flight> query, double specificPrice, double priceMin, double priceMax)
        {
            if (specificPrice != default)
            {
                return query.Where(f => f.EconomyPrice == specificPrice || f.BusinessPrice == specificPrice || f.FirstClassPrice == specificPrice);
            }
            else if (priceMin != default || priceMax != default)
            {
                priceMin = priceMin != default ? priceMin : double.MinValue;
                priceMax = priceMax != default ? priceMax : double.MaxValue;

                return query.Where(f => (f.EconomyPrice >= priceMin && f.EconomyPrice <= priceMax) ||
                                        (f.BusinessPrice >= priceMin && f.BusinessPrice <= priceMax) ||
                                        (f.FirstClassPrice >= priceMin && f.FirstClassPrice <= priceMax));
            }

            return query;
        }
        
        private IQueryable<Flight> FilterFlightByDateRange(IQueryable<Flight> query, DateTime specificDate, DateTime dateMin, DateTime dateMax)
        {
            if (specificDate != default)
            {
                return query.Where(f => f.DepartureDate.Date == specificDate.Date);
            }
            else if (dateMin != default || dateMax != default)
            {
                dateMin = dateMin != default ? dateMin : DateTime.MinValue;
                dateMax = dateMax != default ? dateMax : DateTime.MaxValue;

                return query.Where(f => f.DepartureDate.Date >= dateMin.Date && f.DepartureDate.Date <= dateMax.Date);
            }

            return query;
        }
        
        private IQueryable<Flight> FilterFlightByString(IQueryable<Flight> query, Func<Flight, string> stringSelector, string value)
        {
            return string.IsNullOrEmpty(value) ? query : query
                .Where(f => stringSelector(f)
                    .Equals(value, StringComparison.OrdinalIgnoreCase));
        }

    }
}
