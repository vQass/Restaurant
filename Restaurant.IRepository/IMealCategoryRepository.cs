using Restaurant.DB.Entities;

namespace Restaurant.IRepository
{
    public interface IMealCategoryRepository
    {
        IEnumerable<MealCategory> GetMealCategories();
        MealCategory GetMealCategory(short id);
        short AddMealCategory(MealCategory mealCategory);
        void DeleteMealCategory(MealCategory mealCategory);
        void EnsureMealCategoryExists(MealCategory mealCategory);
        void EnsureMealCategoryNameNotTaken(string mealCategoryName, int id = 0);
        void EnsureMealCategoryNotInUse(MealCategory mealCategory);
        void UpdateMealCategory(MealCategory mealCategory, string newMealCategoryName);
    }
}