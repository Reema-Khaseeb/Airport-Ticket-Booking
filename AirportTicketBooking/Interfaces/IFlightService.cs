using AirportTicketBooking.Enums;
using AirportTicketBooking.Models;

namespace AirportTicketBooking.Interfaces
{
    public interface IFlightService
    {
        void AddFlights(string filePath, UserRole userRole);
        void ViewFlights();
        void ViewSearchFlightsResults(List<Flight> searchResults);
        void SearchFlights(FlightFilterCriteria criteria);
    }
}
