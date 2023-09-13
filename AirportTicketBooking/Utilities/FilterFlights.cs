using AirportTicketBooking.Models;
using AirportTicketBooking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking.Utilities
{
    public static class FilterFlights
    {
        public static IQueryable<Flight> SearchFlights(FlightFilterCriteria criteria)
        {
            Console.WriteLine("----------------- PrintSearchResults ----------------- ");

            var query = GetQueryableFlights();

            query = FilterFlightByPriceRange(query, criteria.SpecificPrice, criteria.PriceRangeMin, criteria.PriceRangeMax);
            query = FilterFlightByDateRange(query, criteria.DepartureDate,
                criteria.DepartureDateRangeMin, criteria.DepartureDateRangeMax);
            query = FilterFlightByString(query, f => f.DepartureCountry, criteria.DepartureCountry);
            query = FilterFlightByString(query, f => f.DestinationCountry, criteria.DestinationCountry);
            query = FilterFlightByString(query, f => f.DepartureAirport, criteria.DepartureAirport);
            query = FilterFlightByString(query, f => f.ArrivalAirport, criteria.ArrivalAirport);

            return query;
        }


        public static IQueryable<Flight> FilterFlightByPriceRange(IQueryable<Flight> query, double specificPrice, double priceMin, double priceMax)
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

        public static IQueryable<Flight> FilterFlightByDateRange(IQueryable<Flight> query, DateTime specificDate, DateTime dateMin, DateTime dateMax)
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

        public static IQueryable<Flight> FilterFlightByDateRange(DateTime specificDate, DateTime dateMin, DateTime dateMax)
        {
            var query = GetQueryableFlights();

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

        public static IQueryable<Flight> FilterFlightByString(IQueryable<Flight> query, Func<Flight, string> stringSelector, string value)
        {
            return string.IsNullOrEmpty(value) ? query : query
                .Where(f => stringSelector(f)
                    .Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        public static IQueryable<Flight> FilterFlightByString(Func<Flight, string> stringSelector, string value)
        {
            var query = GetQueryableFlights();
            return string.IsNullOrEmpty(value) ? query : query
                .Where(f => stringSelector(f)
                    .Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        public static IQueryable<Flight> GetQueryableFlights() => ManagerActions.GetAllFlights().AsQueryable();

    }
}
