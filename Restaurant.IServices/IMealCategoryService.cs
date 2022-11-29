using Restaurant.DB.Entities;

namespace Restaurant.IServices
{
    public interface IMealCategoryService
    {
        IEnumerable<MealCategory> GetMealCategories();
        short AddMealCategory(string mealCategoryName);
        void UpdateMealCategory(short id, string mealCategoryName);
        void DeleteMealCategory(short id);
    }
}
