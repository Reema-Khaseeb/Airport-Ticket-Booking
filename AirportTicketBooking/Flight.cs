using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string DepartureCountry { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }

        public enum FlightClass
        {
            Economy,
            Business,
            FirstClass
        }

        public enum FlightClassPrice
        {
            Economy = 100,
            Business = 200,
            FirstClass = 300
        }




    }
}
