using AirportTicketBooking.Enums;
using AirportTicketBooking.Models;
using AirportTicketBooking.Utilities;
using AirportTicketBooking.Utilities.DataHandler;


namespace AirportTicketBooking.Services
{
    internal class ManagerActions
    {
        private static List<Flight> flights;
        
        public ManagerActions(string filePath = null)
        {
            const string systemFlightsDataFilePath =
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
            
            const string systemFlightsDataFilePath =
                @"C:\\Users\\DELL\\Documents\\GitHub\\Airport-Ticket-Booking\\AirportTicketBooking\\flights.csv";
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
            List<Booking> bookings = BookingService.GetAllBookings();
            var query = bookings.AsQueryable();
            
            query = query.FilterBookingsByFlightNumber(criteria.FlightNumber);
            query = query.FilterByPassportNumber(booking => booking.PassportNumber, criteria.PassportNumber);
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
    }
}
