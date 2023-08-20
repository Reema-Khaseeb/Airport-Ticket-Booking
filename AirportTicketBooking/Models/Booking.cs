using System.ComponentModel.DataAnnotations;
using Airport_Ticket_Booking.Enums;

namespace Airport_Ticket_Booking3.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int FlightNumber { get; set; }
        public required string PassportNumber { get; set; }
        [Required(ErrorMessage = "A class type is required to proceed!")]
        public TicketClass SelectedClass { get; set; }
        public double Price { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
