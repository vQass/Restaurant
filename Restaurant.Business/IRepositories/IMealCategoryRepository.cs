using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.Entities.Entities;

namespace Restaurant.Business.IRepositories
{
    public interface IMealCategoryRepository
    {
        IEnumerable<MealCategory> GetMealCategories(int pageIndex = 0, int pageSize = 0);
        MealCategory GetMealCategory(short id);
        int GetMealCategoriesCount();

        void AddMealCategory(MealCategoryCreateRequest mealCategoryRequest);
        void UpdateMealCategory(MealCategory mealCategory, MealCategoryUpdateRequest mealCategoryRequest);
        void DeleteMealCategory(MealCategory mealCategory);

        void EnsureMealCategoryExists(MealCategory mealCategory);
        void EnsureMealCategoryNameNotTaken(string mealCategoryName, int id = 0);
        void EnsureMealCategoryNotInUse(MealCategory mealCategory);
    }
}