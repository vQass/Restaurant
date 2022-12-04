using FluentValidation;
using Restaurant.Data.Models.CityModels;

namespace Restaurant.Validators.FluentValidators.CityValidators
{
    public class CityCreateRequestValidator : AbstractValidator<CityCreateRequest>
    {
        public CityCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
