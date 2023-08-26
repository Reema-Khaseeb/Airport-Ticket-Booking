using System.ComponentModel.DataAnnotations;

namespace AirportTicketBooking.Utilities.DataHandler.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    internal sealed class AirportCodeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string airportCode)
            {
                return airportCode.Length == 3 && airportCode.All(char.IsUpper);
            }
            return false;
        }
    }
}
