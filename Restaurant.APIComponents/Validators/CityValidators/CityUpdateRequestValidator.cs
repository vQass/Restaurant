using FluentValidation;
using Restaurant.Data.Models.CityModels;

namespace Restaurant.APIComponents.Validators.CityValidators
{
    public class CityUpdateRequestValidator : AbstractValidator<CityUpdateRequest>
    {
        public CityUpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
