using AirportTicketBooking.Enums;
using AirportTicketBooking.Models;
using AirportTicketBooking.Services;

namespace AirportTicketBooking.Utilities
{
    public static class FilterBookings
    {
        public static IQueryable<Booking> FilterBookingsByFlightNumber(this IQueryable<Booking> query, int flightNumber)
        {
            return flightNumber == default ? query : query.Where(booking => booking.FlightNumber == flightNumber);
        }
        /*
        public static IQueryable<Booking> FilterByPassportNumber<Booking>(this IQueryable<Booking> query,
            Func<Booking, string> stringSelector, string value)
        {
            return string.IsNullOrEmpty(value) ? query :
                query
                    .Where(item => stringSelector(item)
                        .Equals(value, StringComparison.OrdinalIgnoreCase));
        }*/

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

        /*public static IQueryable<Booking> FilterBookingsByString(
            this IQueryable<Booking> query,
            Func<Flight, string> stringSelector,
            string value)
        {
            if (string.IsNullOrEmpty(value)) return query;

            var flights = ManagerActions.GetAllFlights();
            return query.Where(booking => flights.Any(flight =>
                flight.FlightNumber == booking.FlightNumber &&
                stringSelector(flight).Equals(value, StringComparison.OrdinalIgnoreCase)));
        }*/
        /**/
        public static IQueryable<Booking> FilterBookingsByString(this IQueryable<Booking> query, Func<Flight, string> stringSelector, string value)
        {
            if (string.IsNullOrEmpty(value))
                return query;

            var flights = FilterFlights.FilterFlightByString(stringSelector, value);            
            var flightNumbers = flights.Select(flight => flight.FlightNumber).ToList();

            return query.Where(booking => flightNumbers.Contains(booking.FlightNumber));
        }
        /*
        public static IQueryable<Booking> FilterBookingsByDateRange(this IQueryable<Booking> query,
            DateTime specificDate, DateTime dateMin, DateTime dateMax)
        {
            var flights = ManagerActions.GetAllFlights();
            if (specificDate != default)
            {
                return query.Where(booking =>
                    flights.Any(flight =>
                        flight.FlightNumber == booking.FlightNumber &&
                        flight.DepartureDate.Date == specificDate.Date));
            }

            if (dateMin == default && dateMax == default)
                return query;

            dateMin = dateMin != default ? dateMin : DateTime.MinValue;
            dateMax = dateMax != default ? dateMax : DateTime.MaxValue;

            return query.Where(booking =>
                flights.Any(flight =>
                    flight.FlightNumber == booking.FlightNumber &&
                    flight.DepartureDate.Date >= dateMin.Date &&
                    flight.DepartureDate.Date <= dateMax.Date));
        }
        */

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

    }
}
