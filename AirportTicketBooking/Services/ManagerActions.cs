using AirportTicketBooking.Enums;
using AirportTicketBooking.Models;
using AirportTicketBooking.Utilities;
using AirportTicketBooking.Models;
using AirportTicketBooking;
using AirportTicketBooking.Services;
using AirportTicketBooking.Utilities.DataHandler;


namespace AirportTicketBooking.Services
{
    internal class ManagerActions
    {
        private static List<Flight> flights;
        
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



        public void FilterBookings(BookingFilterCriteria criteria)
        {
            List<Booking> bookings = BookingService.GetAllBookings();
            var query = bookings.AsQueryable();

            query = FilterBookingsByPriceRange(query, criteria.SpecificPrice, criteria.PriceRangeMin, criteria.PriceRangeMax);
            query = FilterBookingsByDateRange(query, criteria.DepartureDate,
                criteria.DepartureDateRangeMin, criteria.DepartureDateRangeMax);
            query = FilterBookingsByStringProperty(query, f => f.DepartureCountry, criteria.DepartureCountry);
            query = FilterBookingsByStringProperty(query, f => f.DestinationCountry, criteria.DestinationCountry);
            query = FilterBookingsByStringProperty(query, f => f.DepartureAirport, criteria.DepartureAirport);
            query = FilterBookingsByStringProperty(query, f => f.ArrivalAirport, criteria.ArrivalAirport);
            query = FilterByPassportNumber(query, b => b.PassportNumber, criteria.PassportNumber);
            query = FilterBookingsByFlightNumber(query, criteria.FlightNumber);
            query = query.FilterByClassType(criteria.TicketClass);

            ViewFilteredBookings(query.ToList());
        }

        public void ViewFilteredBookings(List<Booking> searchResults)
        {
            if (searchResults == null || searchResults.Count == 0)
            {
                Console.WriteLine("No bookings available.");
                return;
            }

            Console.WriteLine("\n ----------  FilterBookings  ------------------");

            foreach (var booking in searchResults)
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

        private IQueryable<Booking> FilterBookingsByStringProperty(
        IQueryable<Booking> query,
        Func<Flight, string> stringSelector,
        string value)
        {
            if (string.IsNullOrEmpty(value)) return query;

            return query.Where(b => flights
            .Where(f => query.Any(booking => booking.FlightNumber == f.FlightNumber))
            .Any(f => stringSelector(f).Equals(value, StringComparison.OrdinalIgnoreCase)));
        }

        private IQueryable<Booking> FilterByPassportNumber<Booking>(IQueryable<Booking> query, Func<Booking, string> stringSelector, string value)
        {
            return string.IsNullOrEmpty(value) ? query :
                query
                    .Where(item => stringSelector(item)
                        .Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        private IQueryable<Booking> FilterBookingsByDateRange(IQueryable<Booking> query, DateTime specificDate, DateTime dateMin, DateTime dateMax)
        {
            if (specificDate != default)
            {
                return query.Where(b => flights
                    .Where(f => query.Any(booking => booking.FlightNumber == f.FlightNumber))
                    .Any(f => f.DepartureDate.Date == specificDate.Date));
            }

            if (dateMin == default && dateMax == default)
                return query;

            dateMin = dateMin != default ? dateMin : DateTime.MinValue;
            dateMax = dateMax != default ? dateMax : DateTime.MaxValue;

            return query.Where(b => flights
                .Where(f => query.Any(booking => booking.FlightNumber == f.FlightNumber))
                .Any(f => f.DepartureDate.Date >= dateMin.Date && f.DepartureDate.Date <= dateMax.Date));
        }

        private IQueryable<Booking> FilterBookingsByPriceRange(IQueryable<Booking> query, double specificPrice, double priceMin, double priceMax)
        {
            if (specificPrice != default)
            {
                return query.Where(f => f.Price == specificPrice);
            }

            if (priceMin == default && priceMax == default)
                return query;

            priceMin = priceMin != default ? priceMin : double.MinValue;
            priceMax = priceMax != default ? priceMax : double.MaxValue;

            return query.Where(booking => booking
                .Price >= priceMin && booking.Price <= priceMax);
        }

        private IQueryable<Booking> FilterBookingsByFlightNumber(IQueryable<Booking> query, int flightNumber)
        {
            return flightNumber == default ? query : query.Where(booking => booking.FlightNumber == flightNumber);
        }


    }
}
