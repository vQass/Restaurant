using Restaurant.Data.Models.MealModels;
using Restaurant.DB.Entities;

namespace Restaurant.IServices
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetMeals();
        Task<MealAdminPanelWrapper> GetMealsForAdminPanel();
        Task<IEnumerable<MealGroupViewModel>> GetActiveMealsGroupedByCategory();
        MealAdminPanelItem GetMealForAdminPanel(int id);

        int AddMeal(MealCreateRequest mealCreateRequest);
        void UpdateMeal(int id, MealUpdateRequest mealUpdateRequest);
        void DeleteMeal(int id);
        void SetMealAsUnavailable(int id);
        void SetMealAsAvailable(int id);
        void UpdateMealsPrice(int id, decimal newPrice);

    }
}
