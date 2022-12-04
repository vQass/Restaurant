using FluentValidation;
using Restaurant.Data.Models.OrderModels;
using Restaurant.DB;

namespace Restaurant.Validators.FluentValidators.OrderValidators.OrderCreate
{
    public class OrderElementCreateRequestValidator : AbstractValidator<OrderElementCreateRequest>
    {
        public OrderElementCreateRequestValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.MealId).Custom((mealId, context) =>
            {
                var meal = dbContext.Meals.FirstOrDefault(x => x.Id == mealId);

                if (meal is null)
                {
                    context.AddFailure("MealId", "Danie o podanym id nie istnieje.");
                }

                if (!meal.Available)
                {
                    context.AddFailure("MealId", "Danie o podanym id jest nieaktywne.");
                }
            });

            RuleFor(x => x.Amount).Custom((amount, context) =>
            {
                if (amount < 1)
                {
                    context.AddFailure("Amount", "Ilość danego posiłku nie może być mniejsza od 1.");
                }
            });
        }
    }
}
