using AirportTicketBooking.Enums;
using AirportTicketBooking.Interfaces;
using AirportTicketBooking.Models;
using AirportTicketBooking.Utilities;
using AirportTicketBooking.Utilities.DataHandler;


namespace AirportTicketBooking.Services
{
    internal class ManagerActions : IManagerActions
    {
        private const string systemFlightsDataFilePath =
           @"C:\\Users\\DELL\\Documents\\GitHub\\Airport-Ticket-Booking\\AirportTicketBooking\\flights.csv";
        private static List<Flight> flights;
        private static ManagerActions instance;

        public ManagerActions(string filePath = null)
        {
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

        public static ManagerActions Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagerActions();
                }
                return instance;
            }
        }

        public void AddFlights(string filePath, UserRole userRole)
        {
            RoleAuthorization.CheckPermission(userRole, UserRole.Manager);
            var newFlights = FileProcessor.ReadFlightsFromCsv(filePath);            
            FileProcessor.WriteFlightsToCsv(systemFlightsDataFilePath, newFlights);
            flights.AddRange(newFlights);
        }

        public void ViewFlights()
        {
            ViewFlightConsoleStyler.ViewFlights(flights);
        }

        public static List<Flight> GetAllFlights() => flights;

        public void FilterBookings(BookingFilterCriteria criteria)
        {
            var bookings = BookingService.GetAllBookings();
            var query = bookings.AsQueryable();
            
            query = query.FilterBookingsByFlightNumber(criteria.FlightNumber);
            query = query.FilterByPassportNumber(criteria.PassportNumber); // query = query.FilterByPassportNumber(booking => booking.PassportNumber, criteria.PassportNumber);
            query = query.FilterBookingsByPriceRange(criteria.SpecificPrice, criteria.PriceRangeMin, criteria.PriceRangeMax);
            query = query.FilterByClassType(criteria.TicketClass);
            query = query.FilterBookingsByDateRange(criteria.DepartureDate,
                criteria.DepartureDateRangeMin, criteria.DepartureDateRangeMax);
            query = query.FilterBookingsByString(flight => flight.DepartureCountry, criteria.DepartureCountry);
            query = query.FilterBookingsByString(flight => flight.DestinationCountry, criteria.DestinationCountry);
            query = query.FilterBookingsByString(flight => flight.DepartureAirport, criteria.DepartureAirport);
            query = query.FilterBookingsByString(flight => flight.ArrivalAirport, criteria.ArrivalAirport);
            
            ViewFilteredBookings(query.ToList());
        }

        public void ViewFilteredBookings(List<Booking> searchResults)
        {
            Console.WriteLine("\n ----------  FilterBookings  ------------------");
            ConsoleUtility.PrintBookings(searchResults);
        }

        public void ViewSearchFlightsResults(List<Flight> searchResults)
        {
            if (!searchResults.Any())
            {
                Console.WriteLine("No flights found matching the search criteria.");
                return;
            }

            ViewFlightConsoleStyler.ViewFlights(searchResults);

            /*
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
            */
        }

        public void SearchFlights(FlightFilterCriteria criteria)
        {
            var query = FilterFlights.SearchFlights(criteria);
            ViewSearchFlightsResults(query.ToList());
        }
    }
}
