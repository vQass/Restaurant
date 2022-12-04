using FluentValidation;
using Restaurant.Data.Models.MealCategoryModels;

namespace Restaurant.Validators.FluentValidators.MealCategoryValidators
{
    public class MealCategoryCreateRequestValidator : AbstractValidator<MealCategoryCreateRequest>
    {
        public MealCategoryCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
