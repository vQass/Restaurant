using FluentValidation;
using Restaurant.Data.Models.CityModels;

namespace Restaurant.APIComponents.Validators.CityValidators
{
    public class CityCreateRequestValidator : AbstractValidator<CityCreateRequest>
    {
        public CityCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
