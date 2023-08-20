using Airport_Ticket_Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Utilities
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
    }
}
