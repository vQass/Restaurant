using Restaurant.Data.Models.MealCategoryModels;

namespace Restaurant.Business.IServices
{
    public interface IMealCategoryService
    {
        MealCategoryViewModel GetMealCategory(short id);
        List<MealCategoryViewModel> GetMealCategories();
        MealCategoryWrapper GetMealCategoriesPage(int pageIndex, int pageSize);
        void AddMealCategory(MealCategoryCreateRequest mealCategoryRequest);
        void UpdateMealCategory(short id, MealCategoryUpdateRequest mealCategoryRequest);
        void DeleteMealCategory(short id);
    }
}
