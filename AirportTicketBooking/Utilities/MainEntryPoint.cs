using AirportTicketBooking.Enums;
using AirportTicketBooking.Services;

namespace AirportTicketBooking.Utilities
{
    public class MainEntryPoint
    {
        static UserRole GetUserRole()
        {
            Console.WriteLine("Welcome to Airport Ticket Booking System");
            Console.WriteLine("Select your role:");
            Console.WriteLine("1. Passenger");
            Console.WriteLine("2. Manager");
            Console.WriteLine("3. Exit");

            int choice = GetIntInput("Enter your choice: ");

            switch (choice)
            {
                case 1:
                    return UserRole.Passenger;
                case 2:
                    return UserRole.Manager;
                case 3:
                    ExitSystem();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid role.");
                    return GetUserRole();
            }
            return UserRole.None;
        }

        static int GetIntInput(string prompt)
        {
            int input;

            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }
        }

        static void ExitSystem()
        {
            Console.WriteLine("Exiting...");
            Environment.Exit(0);
        }

        public static void FilterBookingsLoop()
        {
            while (true)
            {
                Console.WriteLine("1. Filter Bookings");
                Console.WriteLine("2. Back to Main Menu");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var FlightsFilePath =
                            @"C:\\Users\\DELL\\Documents\\GitHub\\Airport-Ticket-Booking\\AirportTicketBooking\\flights.csv";
                        FlightService flightService = new(FlightsFilePath);

                        var bookingService = new BookingService();

                        var filterCriteria = GetFilterCriteria();
                        bookingService.FilterBookings(filterCriteria);

                        
                        break;
                    case "2":
                        return; // Exit the loop and go back to the main menu
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        public static BookingFilterCriteria GetFilterCriteria()
        {
            var criteria = new BookingFilterCriteria();

            Console.WriteLine("Enter Flight Number (leave empty to skip): ");
            if (int.TryParse(Console.ReadLine(), out int flightNumber))
            {
                criteria.FlightNumber = flightNumber;
            }

            Console.WriteLine("Enter Specific Price (leave empty to skip): ");
            if (double.TryParse(Console.ReadLine(), out double specificPrice))
            {
                criteria.SpecificPrice = specificPrice;
            }

            Console.WriteLine("Enter Price Range Minimum (leave empty to skip): ");
            if (double.TryParse(Console.ReadLine(), out double priceRangeMin))
            {
                criteria.PriceRangeMin = priceRangeMin;
            }

            Console.WriteLine("Enter Price Range Maximum (leave empty to skip): ");
            if (double.TryParse(Console.ReadLine(), out double priceRangeMax))
            {
                criteria.PriceRangeMax = priceRangeMax;
            }

            Console.WriteLine("Enter Departure Country (leave empty to skip): ");
            criteria.DepartureCountry = Console.ReadLine();

            Console.WriteLine("Enter Destination Country (leave empty to skip): ");
            criteria.DestinationCountry = Console.ReadLine();

            Console.WriteLine("Enter Departure Date (yyyy-MM-dd) (leave empty to skip): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime departureDate))
            {
                criteria.DepartureDate = departureDate;
            }

            Console.WriteLine("Enter Departure Date Range Minimum (yyyy-MM-dd) (leave empty to skip): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime departureDateRangeMin))
            {
                criteria.DepartureDateRangeMin = departureDateRangeMin;
            }

            Console.WriteLine("Enter Departure Date Range Maximum (yyyy-MM-dd) (leave empty to skip): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime departureDateRangeMax))
            {
                criteria.DepartureDateRangeMax = departureDateRangeMax;
            }

            Console.WriteLine("Enter Departure Airport (leave empty to skip): ");
            criteria.DepartureAirport = Console.ReadLine();

            Console.WriteLine("Enter Arrival Airport (leave empty to skip): ");
            criteria.ArrivalAirport = Console.ReadLine();

            Console.WriteLine("Enter Passport Number (leave empty to skip): ");
            criteria.PassportNumber = Console.ReadLine();

            criteria = selectTicketClass(criteria);

            return criteria;
        }

        public static BookingFilterCriteria selectTicketClass(BookingFilterCriteria criteria)
        {
            Console.WriteLine("Select Ticket Class:");
            Console.WriteLine("1. Economy");
            Console.WriteLine("2. Business");
            Console.WriteLine("3. First Class");
            Console.WriteLine("4. None (No preference)");
            Console.Write("Enter your choice: ");
            int classChoice = Convert.ToInt32(Console.ReadLine());

            switch (classChoice)
            {
                case 1:
                    criteria.TicketClass = TicketClass.Economy;
                    break;
                case 2:
                    criteria.TicketClass = TicketClass.Business;
                    break;
                case 3:
                    criteria.TicketClass = TicketClass.FirstClass;
                    break;
                case 4:
                    criteria.TicketClass = TicketClass.None;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Setting Ticket Class to None.");
                    criteria.TicketClass = TicketClass.None;
                    break;
            }

            return criteria;
        }

    }
}
