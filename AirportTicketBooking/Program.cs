using AirportTicketBooking;
using AirportTicketBooking.Enums;
using AirportTicketBooking.Models;
using AirportTicketBooking.Services;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("----------------- ManagerActions ----------------- ");
        
        var FlightsFilePath =
            @"C:\\Users\\DELL\\Documents\\GitHub\\Airport-Ticket-Booking\\AirportTicketBooking\\ExistedFlights.csv";
        ManagerActions managerActions = new(FlightsFilePath);

        managerActions.ViewFlights();
        Console.WriteLine("\n\n\n\n");

        var userRole = UserRole.Manager;
        var newFlightsFilePath =
            @"C:\\Users\\DELL\\Documents\\GitHub\\Airport-Ticket-Booking\\AirportTicketBooking\\newFlights.csv";
        managerActions.AddFlights(newFlightsFilePath, userRole);
        managerActions.ViewFlights();

        
        Console.WriteLine("\n-------------------------------------------------- ");
        Console.WriteLine("----------------- BookingService ----------------- ");
        Console.WriteLine("----------------- ----------------- -----------------\n");

        userRole = UserRole.Passenger;
        var bookingService = new BookingService();
        bookingService.PrintAllBookings();
        
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

        Booking booking8 = new()
        {
            FlightNumber = 7,
            PassportNumber = "DEF456",
            SelectedClass = TicketClass.Economy
        };

        bookingService.BookFlight(booking1, userRole);
        bookingService.BookFlight(booking2, userRole);
        bookingService.BookFlight(booking3, userRole);
        bookingService.BookFlight(booking4, userRole);
        bookingService.BookFlight(booking5, userRole);
        bookingService.BookFlight(booking6, userRole);
        bookingService.BookFlight(booking7, userRole);
        bookingService.BookFlight(booking8, userRole);

        bookingService.PrintAllBookings();
        
        var bookingIdToCancel = 3;
        bookingService.CancelBooking(bookingIdToCancel, userRole);
        bookingService.PrintAllBookings();

        var bookingIdToModify = 2;
        var newTicketClass = TicketClass.Business;
        bookingService.ModifyBooking(bookingIdToModify, newTicketClass, userRole);
        bookingService.PrintAllBookings();

        var passengerPassportNumber = "ABC123";
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

        
        Console.WriteLine("\n---- FlightNumber = 1 ---- ");
        BookingFilterCriteria criteria = new()
        {
            FlightNumber = 1
        };
        managerActions.FilterBookings(criteria);

        Console.WriteLine("\n---- SpecificPrice = 1200.0 ---- ");
        BookingFilterCriteria criteria1 = new()
        {
            SpecificPrice = 1200.0
        };
        managerActions.FilterBookings(criteria1);

        Console.WriteLine("\n---- Price = [200.0, 1000.0] ---- ");
        BookingFilterCriteria criteria2 = new()
        {
            PriceRangeMin = 200.0,
            PriceRangeMax = 1000.0
        };
        managerActions.FilterBookings(criteria2);

        Console.WriteLine("\n---- PassportNumber = JKL012 ---- ");
        BookingFilterCriteria criteria9 = new()
        {
            PassportNumber = "JKL012"
        };
        managerActions.FilterBookings(criteria9);
        
        Console.WriteLine("\n---- DestinationCountry = \"Palestine\" ---- ");
        BookingFilterCriteria criteria3 = new()
        {
            DestinationCountry = "Palestine"
        };
        managerActions.FilterBookings(criteria3);

        Console.WriteLine("\n---- DepartureCountry = 'Australia' ---- ");
        BookingFilterCriteria criteria4 = new()
        {
            DepartureCountry = "Australia"
        };
        managerActions.FilterBookings(criteria4);

        Console.WriteLine("\n---- DepartureDate = 2023-09-04 ---- ");
        BookingFilterCriteria criteria5 = new()
        {
            DepartureDate = DateTime.Parse("2023-09-04"),
        };
        managerActions.FilterBookings(criteria5);

        Console.WriteLine("\n---- DepartureDate Range = [Now - 2023-09-04] ---- ");
        BookingFilterCriteria criteria6 = new()
        {
            DepartureDateRangeMin = DateTime.Now,
            DepartureDateRangeMax = DateTime.Parse("2023-09-04")
        };
        managerActions.FilterBookings(criteria6);
        
        Console.WriteLine("\n---- DepartureAirport = \"SYD\" ---- ");
        BookingFilterCriteria criteria7 = new()
        {
            DepartureAirport = "SYD"
        };
        managerActions.FilterBookings(criteria7);

        Console.WriteLine("\n---- ArrivalAirport = 'LHR' ---- ");
        BookingFilterCriteria criteria8 = new()
        {
            ArrivalAirport = "LHR"
        };
        managerActions.FilterBookings(criteria8);
        
        Console.WriteLine("\n---- TicketClass ---- ");
        BookingFilterCriteria criteria10 = new()
        {
            TicketClass = TicketClass.Economy
        };
        managerActions.FilterBookings(criteria10);

        BookingFilterCriteria criteria11 = new()
        {
            TicketClass = TicketClass.Business
        };
        managerActions.FilterBookings(criteria11);

        BookingFilterCriteria criteria12 = new()
        {
            TicketClass = TicketClass.FirstClass
        };
        managerActions.FilterBookings(criteria12);        
    }
}
