using System.ComponentModel.DataAnnotations;

namespace AirportTicketBooking.Utilities.DataHandler.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    internal sealed class DifferentAirportAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;

        public DifferentAirportAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherPropertyValue = validationContext.ObjectType.GetProperty(_otherProperty)?.GetValue(validationContext.ObjectInstance, null);

            if (value is string thisAirport && otherPropertyValue is string otherAirport && thisAirport == otherAirport)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
