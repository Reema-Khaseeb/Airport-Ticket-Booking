using Airport_Ticket_Booking;

class Program
{
    static void Main(string[] args)
    {
        Flight flight1 = new(1, "USA", "UK", DateTime.Now.AddDays(7), "JFK", "LHR",
                       new Dictionary<Flight.TicketClass, double> {
                           { Flight.TicketClass.Economy, 500.0 },
                           { Flight.TicketClass.Business, 1000.0 },
                           { Flight.TicketClass.FirstClass, 1500.0 }
                       });

        Flight flight2 = new(2, "France", "Spain", DateTime.Now.AddDays(14), "CDG", "BCN",
                       new Dictionary<Flight.TicketClass, double> {
                           { Flight.TicketClass.Economy, 400.0 },
                           { Flight.TicketClass.Business, 800.0 },
                           { Flight.TicketClass.FirstClass, 1200.0 }
                       });

        Flight flight3 = new(3, "Germany", "Italy", DateTime.Now.AddDays(21), "FRA", "FCO",
                       new Dictionary<Flight.TicketClass, double> {
                           { Flight.TicketClass.Economy, 350.0 },
                           { Flight.TicketClass.Business, 700.0 },
                           { Flight.TicketClass.FirstClass, 1100.0 }
                       });

        List<Flight> initialFlights = new List<Flight>
        {
            flight1, flight2, flight3
        };


        // ManagerActions initialFlights = new ManagerActions();
        ManagerActions flights = new ManagerActions(initialFlights);

        flights.ViewFlights();

        Console.WriteLine("\n\n\n\n");

        Flight flight4 = new(4, "Canada", "Japan", DateTime.Now.AddDays(30), "YYZ", "HND",
                       new Dictionary<Flight.TicketClass, double>
                       {
                           { Flight.TicketClass.Economy, 600.0 },
                           { Flight.TicketClass.Business, 1200.0 },
                           { Flight.TicketClass.FirstClass, 1800.0 }
                       });

        Flight flight5 = new(5, "Australia", "New Zealand", DateTime.Now.AddDays(45), "SYD", "AKL",
                   new Dictionary<Flight.TicketClass, double>
                   {
                           { Flight.TicketClass.Economy, 300.0 },
                           { Flight.TicketClass.Business, 600.0 },
                           { Flight.TicketClass.FirstClass, 900.0 }
                   });

        List<Flight> newFlights = new List<Flight>
        {
            flight4, flight5
        };

        flights.AddFlights(newFlights);
        flights.ViewFlights();
    }
}
