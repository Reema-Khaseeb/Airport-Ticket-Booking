using AirportTicketBooking.Enums;
using AirportTicketBooking.Models;
using AirportTicketBooking.Services;

namespace AirportTicketBooking.Utilities
{
    public static class FilterBookingsUtility
    {
        public static IQueryable<Booking> FilterBookings(BookingFilterCriteria criteria)
        {
            var query = GetQueryableBookings();

            query = query.FilterBookingsByFlightNumber(criteria.FlightNumber);
            query = query.FilterByPassportNumber(criteria.PassportNumber);
            query = query.FilterBookingsByPriceRange(criteria.SpecificPrice, criteria.PriceRangeMin, criteria.PriceRangeMax);
            query = query.FilterByClassType(criteria.TicketClass);
            query = query.FilterBookingsByDateRange(criteria.DepartureDate,
                criteria.DepartureDateRangeMin, criteria.DepartureDateRangeMax);
            query = query.FilterBookingsByString(flight => flight.DepartureCountry, criteria.DepartureCountry);
            query = query.FilterBookingsByString(flight => flight.DestinationCountry, criteria.DestinationCountry);
            query = query.FilterBookingsByString(flight => flight.DepartureAirport, criteria.DepartureAirport);
            query = query.FilterBookingsByString(flight => flight.ArrivalAirport, criteria.ArrivalAirport);

            return query;
        }

        public static IQueryable<Booking> FilterBookingsByFlightNumber(this IQueryable<Booking> query, int flightNumber)
        {
            return flightNumber == default ? query : query.Where(booking => booking.FlightNumber == flightNumber);
        }

        public static IQueryable<Booking> FilterByPassportNumber(this IQueryable<Booking> query, string value)
        {
            return string.IsNullOrEmpty(value) ? query : 
                query
                .Where(booking => booking.PassportNumber
                .Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        public static IQueryable<Booking> FilterBookingsByPriceRange(this IQueryable<Booking> query,
            double specificPrice, double priceMin, double priceMax)
        {
            if (specificPrice != default)
            {
                return query.Where(booking => booking.Price == specificPrice);
            }

            if (priceMin == default && priceMax == default)
                return query;

            priceMin = priceMin != default ? priceMin : double.MinValue;
            priceMax = priceMax != default ? priceMax : double.MaxValue;

            return query.Where(booking => booking
                .Price >= priceMin && booking.Price <= priceMax);
        }

        public static IQueryable<Booking> FilterByClassType(this IQueryable<Booking> query, TicketClass ticketClass)
        {
            if (ticketClass == TicketClass.None || string.IsNullOrWhiteSpace(ticketClass.ToString()))
            {
                return query;
            }

            return query.Where(booking => booking.SelectedClass == ticketClass);
        }

        public static IQueryable<Booking> FilterBookingsByString(this IQueryable<Booking> query, Func<Flight, string> stringSelector, string value)
        {
            if (string.IsNullOrEmpty(value))
                return query;

            var flights = FilterFlights.FilterFlightByString(stringSelector, value);            
            var flightNumbers = flights.Select(flight => flight.FlightNumber).ToList();

            return query.Where(booking => flightNumbers.Contains(booking.FlightNumber));
        }
        
        public static IQueryable<Booking> FilterBookingsByDateRange(
            this IQueryable<Booking> query,
            DateTime specificDate, DateTime dateMin, DateTime dateMax)
        {
            if (dateMin == default && dateMax == default && specificDate == default)
                return query;

            var flights = FilterFlights.FilterFlightByDateRange(specificDate, dateMin, dateMax);
            var flightNumbers = flights.Select(flight => flight.FlightNumber).ToList();

            if (specificDate != default)
            {                
                return query.Where(booking => flightNumbers.Contains(booking.FlightNumber));
            }

            return query.Where(booking => flightNumbers.Contains(booking.FlightNumber));
        }

        private static IQueryable<Booking> GetQueryableBookings() => BookingService.GetAllBookings().AsQueryable();
    }
}
