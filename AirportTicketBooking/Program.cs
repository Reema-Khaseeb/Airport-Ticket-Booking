using Airport_Ticket_Booking.Enums;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Services;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("----------------- ManagerActions ----------------- ");

        Flight flight1 = new()
        {
            FlightNumber = 1,
            DepartureCountry = "USA",
            DestinationCountry = "UK",
            DepartureDate = DateTime.Now.AddDays(7),
            DepartureAirport = "JFK",
            ArrivalAirport = "LHR",
            EconomyPrice = 500.0,
            BusinessPrice = 1000.0,
            FirstClassPrice = 1500.0
        };

        Flight flight2 = new()
        {
            FlightNumber = 2,
            DepartureCountry = "France",
            DestinationCountry = "Spain",
            DepartureDate = DateTime.Now.AddDays(14),
            DepartureAirport = "CDG",
            ArrivalAirport = "BCN",
            EconomyPrice = 400.0,
            BusinessPrice = 800.0,
            FirstClassPrice = 1200.0
        };

        Flight flight3 = new()
        {
            FlightNumber = 3,
            DepartureCountry = "Germany",
            DestinationCountry = "Italy",
            DepartureDate = DateTime.Now.AddDays(21),
            DepartureAirport = "FRA",
            ArrivalAirport = "FCO",
            EconomyPrice = 350.0,
            BusinessPrice = 700.0,
            FirstClassPrice = 1100.0
        };

        List<Flight> initialFlights = new()
        {
            flight1, flight2, flight3
        };

        // ManagerActions initialFlights = new ManagerActions();
        ManagerActions flights = new(initialFlights);

        flights.ViewFlights();

        Console.WriteLine("\n\n\n\n");


        Flight flight4 = new()
        {
            FlightNumber = 4,
            DepartureCountry = "Canada",
            DestinationCountry = "Japan",
            DepartureDate = DateTime.Now.AddDays(30),
            DepartureAirport = "YYZ",
            ArrivalAirport = "HND",
            EconomyPrice = 600.0,
            BusinessPrice = 1200.0,
            FirstClassPrice = 1800.0
        };

        Flight flight5 = new()
        {
            FlightNumber = 5,
            DepartureCountry = "Australia",
            DestinationCountry = "New Zealand",
            DepartureDate = DateTime.Now.AddDays(45),
            DepartureAirport = "SYD",
            ArrivalAirport = "AKL",
            EconomyPrice = 300.0,
            BusinessPrice = 600.0,
            FirstClassPrice = 900.0
        };

        Flight flight6 = new()
        {
            FlightNumber = 6,
            DepartureCountry = "Australia",
            DestinationCountry = "Palestine",
            DepartureDate = DateTime.Now.AddDays(17),
            DepartureAirport = "SYD",
            ArrivalAirport = "HND",
            EconomyPrice = 400.0,
            BusinessPrice = 750.0,
            FirstClassPrice = 1200.0
        };

        Flight flight7 = new()
        {
            FlightNumber = 7,
            DepartureCountry = "Canada",
            DestinationCountry = "Palestine",
            DepartureDate = DateTime.Now.AddDays(6),
            DepartureAirport = "JFK",
            ArrivalAirport = "AKL",
            EconomyPrice = 400.0,
            BusinessPrice = 750.0,
            FirstClassPrice = 1200.0
        };

        List<Flight> newFlights = new()
        {
            flight4, flight5, flight6, flight7
        };

        var userRole = UserRole.Manager;
        flights.AddFlights(newFlights, userRole);
        flights.ViewFlights();
    }
}
