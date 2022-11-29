using FluentValidation;
using Restaurant.Data.Models.IngredientModels;

namespace Restaurant.APIComponents.Validators.IngredientValidators
{
    public class IngredientUpdateRequestValidator : AbstractValidator<IngredientUpdateRequest>
    {
        public IngredientUpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
