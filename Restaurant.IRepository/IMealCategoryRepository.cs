using Restaurant.DB.Entities;

namespace Restaurant.IRepository
{
    public interface IMealCategoryRepository
    {
        IEnumerable<MealCategory> GetMealCategories(int pageIndex = 0, int pageSize = 0);
        MealCategory GetMealCategory(short id);
        int GetMealCategoriesCount();

        short AddMealCategory(MealCategory mealCategory);
        void UpdateMealCategory(MealCategory mealCategory, string newMealCategoryName);
        void DeleteMealCategory(MealCategory mealCategory);

        void EnsureMealCategoryExists(MealCategory mealCategory);
        void EnsureMealCategoryNameNotTaken(string mealCategoryName, int id = 0);
        void EnsureMealCategoryNotInUse(MealCategory mealCategory);
    }
}