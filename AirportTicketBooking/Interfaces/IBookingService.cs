using AirportTicketBooking.Enums;
using AirportTicketBooking.Models;

namespace AirportTicketBooking.Interfaces
{
    public interface IBookingService
    {
        void BookFlight(Booking newBooking, UserRole userRole);
        void CancelBooking(int bookingId, UserRole userRole);
        void ModifyBooking(int bookingId, TicketClass newTicketClass, UserRole userRole);
        void ViewPassengerBookings(string passportNumber);
        List<Booking> GetPassengerBookings(string passportNumber);
        void ViewAllBookings();        
    }
}
