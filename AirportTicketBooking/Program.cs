using Airport_Ticket_Booking;
using Airport_Ticket_Booking.Enums;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Services;
using Airport_Ticket_Booking3.Models;
using AirportTicketBooking.Services;

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


        Console.WriteLine("\n-------------------------------------------------- ");
        Console.WriteLine("----------------- BookingService ----------------- ");
        Console.WriteLine("----------------- ----------------- -----------------\n");

        userRole = UserRole.Passenger;
        BookingService bookingService = new BookingService();

        bookingService.PrintAllBookings();

        // Create individual Booking objects
        Booking booking1 = new()
        {
            FlightNumber = 1,
            PassportNumber = "ABC123",
            SelectedClass = TicketClass.Business
        };

        Booking booking2 = new()
        {
            FlightNumber = 2,
            PassportNumber = "XYZ789",
            SelectedClass = TicketClass.Economy
        };

        Booking booking3 = new()
        {
            FlightNumber = 3,
            PassportNumber = "DEF456",
            SelectedClass = TicketClass.FirstClass
        };

        Booking booking4 = new()
        {
            FlightNumber = 4,
            PassportNumber = "GHI789",
            SelectedClass = TicketClass.Business
        };

        Booking booking5 = new()
        {
            FlightNumber = 5,
            PassportNumber = "JKL012",
            SelectedClass = TicketClass.Economy
        };

        Booking booking6 = new()
        {
            FlightNumber = 6,
            PassportNumber = "ABC123",
            SelectedClass = TicketClass.FirstClass
        };

        Booking booking7 = new()
        {
            FlightNumber = 1,
            PassportNumber = "JKL012",
            SelectedClass = TicketClass.FirstClass
        };

        // Add each booking to the BookingService
        bookingService.BookFlight(booking1, userRole);
        bookingService.BookFlight(booking2, userRole);
        bookingService.BookFlight(booking3, userRole);
        bookingService.BookFlight(booking4, userRole);
        bookingService.BookFlight(booking5, userRole);
        bookingService.BookFlight(booking6, userRole);
        bookingService.BookFlight(booking7, userRole);

        bookingService.PrintAllBookings();



        int bookingIdToCancel = 3;
        bookingService.CancelBooking(bookingIdToCancel, userRole);

        bookingService.PrintAllBookings();


        int bookingIdToModify = 2;
        TicketClass newTicketClass = TicketClass.Business;
        bookingService.ModifyBooking(bookingIdToModify, newTicketClass, userRole);

        bookingService.PrintAllBookings();



        string passengerPassportNumber = "ABC123";
        bookingService.ViewPassengerBookings(passengerPassportNumber);
        bookingService.ViewPassengerBookings("JKL012");



        Console.WriteLine("\n---- with no filter ---- ");
        FlightFilterCriteria searchCriteria0 = new() { };
        bookingService.SearchFlights(searchCriteria0);


        FlightFilterCriteria searchCriteria1 = new()
        {
            DepartureCountry = "USA",
            DestinationCountry = "UK",
            DepartureAirport = "JFK",
            ArrivalAirport = "LHR",
        };
        bookingService.SearchFlights(searchCriteria1);


        FlightFilterCriteria searchCriteria2 = new()
        {
            DepartureCountry = "Australia",
            DestinationCountry = "Palestine",
        };
        bookingService.SearchFlights(searchCriteria2);


        FlightFilterCriteria searchCriteria3 = new()
        {
            DepartureCountry = "Australia",
        };
        bookingService.SearchFlights(searchCriteria3);


        FlightFilterCriteria searchCriteria4 = new()
        {
            SpecificPrice = 300.0,
            DepartureCountry = "Australia",
        };
        bookingService.SearchFlights(searchCriteria4);



        FlightFilterCriteria searchCriteria5 = new()
        {
            PriceRangeMin = 500.0,
            PriceRangeMax = 1200.0,
        };
        bookingService.SearchFlights(searchCriteria5);


        FlightFilterCriteria searchCriteria8 = new()
        {
            PriceRangeMin = 1200.0,
        };
        bookingService.SearchFlights(searchCriteria8);


        FlightFilterCriteria searchCriteria6 = new()
        {
            DepartureAirport = "JFK"
        };
        bookingService.SearchFlights(searchCriteria6);


        FlightFilterCriteria searchCriteria7 = new()
        {
            ArrivalAirport = "HND"
        };
        bookingService.SearchFlights(searchCriteria7);


        FlightFilterCriteria searchCriteria9 = new()
        {
            SpecificPrice = 350.0,
        };
        bookingService.SearchFlights(searchCriteria9);

        FlightFilterCriteria searchCriteria10 = new()
        {
            DepartureDate = DateTime.Parse("2023-09-03"),
        };
        bookingService.SearchFlights(searchCriteria10);


        FlightFilterCriteria searchCriteria11 = new()
        {
            DepartureDateRangeMin = DateTime.Parse("2023-08-15"),
            DepartureDateRangeMax = DateTime.Parse("2023-09-03"),
        };
        bookingService.SearchFlights(searchCriteria11);

    }
}
