using FluentValidation;
using Restaurant.Data.Models.IngredientModels;

namespace Restaurant.Validators.FluentValidators.IngredientValidators
{
    public class IngredientCreateRequestValidator : AbstractValidator<IngredientCreateRequest>
    {
        public IngredientCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
