using Airport_Ticket_Booking.Enums;

namespace Airport_Ticket_Booking
{
    public class FlightFilterCriteria
    {
        public double? SpecificPrice { get; set; }
        public double? PriceRangeMin { get; set; }
        public double? PriceRangeMax { get; set; }
        public string? DepartureCountry { get; set; }
        public string? DestinationCountry { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? DepartureDateRangeMin { get; set; }
        public DateTime? DepartureDateRangeMax { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
    }
}