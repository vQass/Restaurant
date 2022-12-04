using FluentValidation;
using Restaurant.Data.Models.CityModels;

namespace Restaurant.Validators.FluentValidators.CityValidators
{
    public class CityUpdateRequestValidator : AbstractValidator<CityUpdateRequest>
    {
        public CityUpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
