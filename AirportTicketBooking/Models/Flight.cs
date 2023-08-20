using System.ComponentModel.DataAnnotations;

namespace Airport_Ticket_Booking.Models
{
    public class Flight
    {
        [Key]
        public int FlightNumber { get; set; }
        [Required(ErrorMessage = "A flight's Departure Country is required to proceed!")]
        public string DepartureCountry { get; set; }
        [Required(ErrorMessage = "A flight's Destination Country is required to proceed!")]
        public string DestinationCountry { get; set; }
        [Required(ErrorMessage = "A flight's Departure Date is required to proceed!")]
        //TODO: [CurrentDate(ErrorMessage = "departure date must be in the future!")]
        public DateTime DepartureDate { get; set; }
        [Required(ErrorMessage = "A flight's Departure Airport is required to proceed!")]
        public string DepartureAirport { get; set; }
        [Required(ErrorMessage = "A flight's Arrival Airport is required to proceed!")]
        public string ArrivalAirport { get; set; }
        [Required(ErrorMessage = "The 'Price' for flight's Economy Class is required to proceed!")]
        public double EconomyPrice { get; set; }
        [Required(ErrorMessage = "The 'Price' for flight's Business Class is required to proceed!")]
        public double BusinessPrice { get; set; }
        [Required(ErrorMessage = "The 'Price' for flight's First Class is required to proceed!")]
        public double FirstClassPrice { get; set; }
    }
}
