using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.ServiceContracts.Enums;

namespace WorkFinder.ServiceContracts.Attributes
{
    public class EnumValidationAttribute : ValidationAttribute
    {
        private readonly Type _enumType;
        public EnumValidationAttribute(Type enumType)
        {
            _enumType = enumType; 
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{validationContext.MemberName} is required.");
            }

            if (value is string stringValue)
            {
                // Try parsing string to enum
                if (Enum.TryParse(_enumType, stringValue, true, out var parsed) &&
                    Enum.IsDefined(_enumType, parsed))
                {
                    return ValidationResult.Success;
                }

                var allowedValues = string.Join(", ", Enum.GetNames(_enumType));
                return new ValidationResult($"Invalid value '{stringValue}'. Allowed values are: {allowedValues}");
            }

            return new ValidationResult($"Invalid value type for {validationContext.MemberName}");
        }
    }
}
