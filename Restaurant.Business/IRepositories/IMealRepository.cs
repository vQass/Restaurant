using Restaurant.Data.Models.MealModels;
using Restaurant.Entities.Entities;

namespace Restaurant.Business.IRepositories
{
    public interface IMealRepository
    {
        Meal GetMeal(int id);
        Task<IEnumerable<Meal>> GetMeals(int pageIndex = 0, int pageSize = 0);
        Task<IEnumerable<MealCategory>> GetMealsGroupedByCategory();
        int GetMealsCount();

        void AddMeal(MealCreateRequest mealCreateRequest);
        void UpdateMeal(Meal meal, MealUpdateRequest mealUpdateRequest);
        void UpdateMealsPrice(Meal meal, decimal newPrice);
        void DeleteMeal(Meal meal);

        void SetMealAsAvailable(Meal meal);
        void SetMealAsUnavailable(Meal meal);

        void EnsureMealExists(Meal meal);
        void EnsureMealNameNotTaken(string mealName, int id = 0);
        void EnsureMealNotInUse(Meal meal);
    }
}