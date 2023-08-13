using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking
{
    public class Flight
    {
        public int FlightNumber { get; set; }
        public string DepartureCountry { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public enum TicketClass
        {
            Economy,
            Business,
            FirstClass
        }
        public Dictionary<TicketClass, double> ClassPrices { get; set; } = new Dictionary<TicketClass, double>
        {
            {TicketClass.Economy, 0.0},
            {TicketClass.Business, 0.0},
            {TicketClass.FirstClass, 0.0}
        };

        public Flight(int flightNumber, string departureCountry, string destinationCountry,
                  DateTime departureDate, string departureAirport, string arrivalAirport,
                  Dictionary<TicketClass, double> classPrices)
        {
            FlightNumber = flightNumber;
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureDate = departureDate;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            ClassPrices = classPrices;
        }
    }
}
