using Restaurant.Data.Models.MealModels;

namespace Restaurant.Business.IServices
{
    public interface IMealService
    {
        MealViewModel GetMeal(int id);
        Task<MealWrapper> GetMealPage(int pageIndex, int pageSize);
        Task<IEnumerable<MealGroupViewModel>> GetActiveMealsGroupedByCategory();

        void AddMeal(MealCreateRequest mealCreateRequest);
        void UpdateMeal(int id, MealUpdateRequest mealUpdateRequest);
        void UpdateMealsPrice(int id, decimal newPrice);
        void DeleteMeal(int id);

        void SetMealAsUnavailable(int id);
        void SetMealAsAvailable(int id);
    }
}
