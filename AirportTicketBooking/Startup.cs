using Microsoft.Extensions.Configuration;

namespace AirportTicketBooking
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
