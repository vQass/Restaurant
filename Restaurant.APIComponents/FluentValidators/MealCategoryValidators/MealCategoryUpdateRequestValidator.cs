using FluentValidation;
using Restaurant.Data.Models.MealCategoryModels;

namespace Restaurant.Validators.FluentValidators.MealCategoryValidators
{
    public class MealCategoryUpdateRequestValidator : AbstractValidator<MealCategoryUpdateRequest>
    {
        public MealCategoryUpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
