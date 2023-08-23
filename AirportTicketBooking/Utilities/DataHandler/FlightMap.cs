using Airport_Ticket_Booking.Models;
using CsvHelper.Configuration;

namespace AirportTicketBooking.Utilities.DataHandler 
{
    internal sealed class FlightMap : ClassMap<Flight>
    {
        public FlightMap()
        {
            Map(m => m.FlightNumber).Name("FlightNumber");
            Map(m => m.DepartureCountry).Name("DepartureCountry");
            Map(m => m.DestinationCountry).Name("DestinationCountry");
            Map(m => m.DepartureDate).Name("DepartureDate");
            Map(m => m.DepartureAirport).Name("DepartureAirport");
            Map(m => m.ArrivalAirport).Name("ArrivalAirport");
            Map(m => m.EconomyPrice).Name("EconomyPrice");
            Map(m => m.BusinessPrice).Name("BusinessPrice");
            Map(m => m.FirstClassPrice).Name("FirstClassPrice");
        }
    }
}
