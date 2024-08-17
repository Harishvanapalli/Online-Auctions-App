using System.ComponentModel.DataAnnotations;

namespace Online_Auction.API.Helpers
{
    public class CustomValidationAttributes : ValidationAttribute
    {
        private readonly string[] _productCategories;

        public CustomValidationAttributes(params string[] productCategories)
        {
            _productCategories = productCategories;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var Category = value as string;

            if (_productCategories.Contains(Category))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Allowed Categories are: {string.Join(", ", _productCategories)}");
        }
    }

    public class ReservedPriceValidation : ValidationAttribute
    {
        private readonly string _startingPricePropertyName;

        public ReservedPriceValidation(string _startingPricePropertyName)
        {
            this._startingPricePropertyName = _startingPricePropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var reservedPrice = (double)value;

            var startingPriceProperty = validationContext.ObjectType.GetProperty(_startingPricePropertyName);
            if (startingPriceProperty == null)
            {
                return new ValidationResult($"Unknown property: {_startingPricePropertyName}");
            }
            var startingPriceValue = (double)startingPriceProperty.GetValue(validationContext.ObjectInstance);

            if(reservedPrice <= startingPriceValue)
            {
                return new ValidationResult("Reserved Price must be greater than starting price");
            }
            return ValidationResult.Success;
        }
    }

}
