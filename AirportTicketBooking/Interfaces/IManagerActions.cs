using AirportTicketBooking.Enums;
using AirportTicketBooking.Models;
using AirportTicketBooking.Services;

namespace AirportTicketBooking.Interfaces
{
    public interface IManagerActions
    {
        void AddFlights(string filePath, UserRole userRole);
        void ViewFlights();
        void FilterBookings(BookingFilterCriteria criteria);
        void ViewFilteredBookings(List<Booking> searchResults);
        void ViewSearchFlightsResults(List<Flight> searchResults);
        void SearchFlights(FlightFilterCriteria criteria);
    }
}
