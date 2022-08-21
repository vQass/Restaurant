using Restaurant.Data.Models.MealModels;
using Restaurant.DB.Entities;

namespace Restaurant.Repository.Repositories
{
    public interface IMealRepository
    {
        int AddMeal(MealCreateRequest mealCreateRequest);
        void DeleteMeal(Meal meal);
        void EnsureMealExists(Meal meal);
        void EnsureMealNameNotTaken(string mealName, int id = 0);
        void EnsureMealNotInUse(Meal meal);
        Meal GetMeal(int id);
        Task<IEnumerable<Meal>> GetMeals();
        void SetMealAsAvailable(Meal meal);
        void SetMealAsUnavailable(Meal meal);
        void UpdateMeal(Meal meal, MealUpdateRequest mealUpdateRequest);
        void UpdateMealsPrice(Meal meal, decimal newPrice);
    }
}