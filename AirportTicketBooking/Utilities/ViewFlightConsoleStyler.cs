using AirportTicketBooking.Models;

namespace AirportTicketBooking.Utilities
{
    public static class ViewFlightConsoleStyler
    {
        private const int SmallColumnWidth = 10;
        private const int MediumColumnWidth = 20;
        private const int LargeColumnWidth = 15;
        private const int TotalTableWidth = 170;

        public static void PrintFlightHeader()
        {
            Console.WriteLine("Available Flights:");
            Console.WriteLine();
            Console.WriteLine($"| {"Flight No.",-SmallColumnWidth} | " +
                          $"{"Departure Country",-MediumColumnWidth} | " +
                          $"{"Destination Country",-MediumColumnWidth} | " +
                          $"{"Departure Date",-MediumColumnWidth} | " +
                          $"{"Departure Airport",-MediumColumnWidth} | " +
                          $"{"Arrival Airport",-MediumColumnWidth} | " +
                          $"{"Economy",-SmallColumnWidth} | " +
                          $"{"Business",-SmallColumnWidth} | " +
                          $"{"FirstClass",-SmallColumnWidth} |");
        }

        public static void PrintHorizontalLine()
        {
            Console.WriteLine(new string('-', TotalTableWidth));
        }

        public static void PrintFlightRow(Flight flight)
        {
            Console.WriteLine($"| {flight.FlightNumber,-SmallColumnWidth} | " +
                          $"{flight.DepartureCountry,-MediumColumnWidth} | " +
                          $"{flight.DestinationCountry,-MediumColumnWidth} | " +
                          $"{flight.DepartureDate,-MediumColumnWidth:yyyy-MM-dd} | " +
                          $"{flight.DepartureAirport,-MediumColumnWidth} | " +
                          $"{flight.ArrivalAirport,-MediumColumnWidth} | " +
                          $"{flight.EconomyPrice,-SmallColumnWidth} | " +
                          $"{flight.BusinessPrice,-SmallColumnWidth} | " +
                          $"{flight.FirstClassPrice,-SmallColumnWidth} |");
        }

        public static void ViewFlights(List<Flight> flights)
        {
            if (flights == null || flights.Count == 0)
            {
                Console.WriteLine("No flights found.");
                return;
            }

            PrintFlightHeader();
            PrintHorizontalLine();
            foreach (var flight in flights)
            {
                PrintFlightRow(flight);
                PrintHorizontalLine();
            }
        }

    }
}
