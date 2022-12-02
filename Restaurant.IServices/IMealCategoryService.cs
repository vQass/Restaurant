using Restaurant.DB.Entities;

namespace Restaurant.IServices
{
    public interface IMealCategoryService
    {
        MealCategoryViewModel GetMealCategory(short id);
        IEnumerable<MealCategory> GetMealCategories();
        MealCategoryWrapper GetMealCategoriesPage(int pageIndex, int pageSize);
        short AddMealCategory(string mealCategoryName);
        void UpdateMealCategory(short id, string mealCategoryName);
        void DeleteMealCategory(short id);
    }
}
