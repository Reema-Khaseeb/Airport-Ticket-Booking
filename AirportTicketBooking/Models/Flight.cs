using System.ComponentModel.DataAnnotations;
using AirportTicketBooking.Utilities.DataHandler.Validations;

namespace Airport_Ticket_Booking.Models
{
    public class Flight
    {
        [Key]
        public int FlightNumber { get; set; }

        [Required(ErrorMessage = "Departure Country is required.")]
        public string DepartureCountry { get; set; }

        [Required(ErrorMessage = "Destination Country is required.")]
        public string DestinationCountry { get; set; }

        [Required(ErrorMessage = "Departure Date is required.")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "Departure Date must be in the future.")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Departure Airport is required.")]
        [AirportCode(ErrorMessage = "Departure Airport Code must be three uppercase letters.")]
        public string DepartureAirport { get; set; }

        [Required(ErrorMessage = "Arrival Airport is required.")]
        [AirportCode(ErrorMessage = "Arrival Airport Code must be three uppercase letters.")]
        [DifferentAirport(nameof(DepartureAirport),
            ErrorMessage = "Departure and Arrival Airports cannot be the same.")]
        public string ArrivalAirport { get; set; }

        [Required(ErrorMessage = "Economy Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Economy Price must be a non-negative value.")]
        public double EconomyPrice { get; set; }

        [Required(ErrorMessage = "Business Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Business Price must be a non-negative value.")]
        public double BusinessPrice { get; set; }

        [Required(ErrorMessage = "First Class Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "First Class Price must be a non-negative value.")]
        public double FirstClassPrice { get; set; }
    }
}