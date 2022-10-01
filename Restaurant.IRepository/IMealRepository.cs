using Restaurant.Data.Models.MealModels;
using Restaurant.DB.Entities;

namespace Restaurant.IRepository
{
    public interface IMealRepository
    {
        Meal GetMeal(int id);
        Task<IEnumerable<Meal>> GetMeals();
        Task<IEnumerable<MealCategory>> GetMealsGroupedByCategory();

        int AddMeal(MealCreateRequest mealCreateRequest);
        void DeleteMeal(Meal meal);

        void SetMealAsAvailable(Meal meal);
        void SetMealAsUnavailable(Meal meal);
        void UpdateMeal(Meal meal, MealUpdateRequest mealUpdateRequest);
        void UpdateMealsPrice(Meal meal, decimal newPrice);

        void EnsureMealExists(Meal meal);
        void EnsureMealNameNotTaken(string mealName, int id = 0);
        void EnsureMealNotInUse(Meal meal);
    }
}