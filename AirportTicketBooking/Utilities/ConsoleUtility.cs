using AirportTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking.Utilities
{
    public static class ConsoleUtility
    {
        public static void PrintBookings(List<Booking> bookings)
        {
            if (bookings == null || bookings.Count == 0)
            {
                Console.WriteLine("No bookings available.");
                return;
            }

            foreach (var booking in bookings)
            {
                PrintBookingDetails(booking);
            }
        }

        private static void PrintBookingDetails(Booking booking)
        {
            Console.WriteLine($"Booking ID: {booking.BookingId}");
            Console.WriteLine($"Flight Number: {booking.FlightNumber}");
            Console.WriteLine($"Passport Number: {booking.PassportNumber}");
            Console.WriteLine($"Price: {booking.Price}");
            Console.WriteLine($"Booking Date: {booking.BookingDate}");
            Console.WriteLine($"Selected Class: {booking.SelectedClass}");
            Console.WriteLine();
        }
    }

}
