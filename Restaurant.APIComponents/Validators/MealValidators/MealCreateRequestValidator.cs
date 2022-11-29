using FluentValidation;
using Restaurant.Data.Models.MealModels;
using Restaurant.DB;

namespace Restaurant.APIComponents.Validators.MealValidators
{
    public class MealCreateRequestValidator : AbstractValidator<MealCreateRequest>
    {
        public MealCreateRequestValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Price).GreaterThan(0).NotNull();

            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);

            RuleFor(x => x.Name).Custom((value, context) =>
            {
                var nameInUse = dbContext.Meals.Any(x =>
                    x.Name.ToLower().Trim() == value.ToLower().Trim());

                if (nameInUse)
                {
                    context.AddFailure("Name", "Nazwa dania jest zajęta");
                }
            });

            RuleFor(x => x.MealCategoryId).Custom((value, context) =>
            {
                var validCategoryId = dbContext.MealsCategories.Any(x =>
                    x.Id == value);

                if (!validCategoryId)
                {
                    context.AddFailure("CategoryId", "Podana kategoria dania nie istnieje");
                }
            });
        }
    }
}
