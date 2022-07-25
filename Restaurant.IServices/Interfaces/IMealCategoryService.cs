using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.IServices.Interfaces
{
    public interface IMealCategoryService
    {
        List<MealCategory> GetAllMealsCategories();
        short AddMealCategory(MealCategoryCreateRequest mealCategory);
        void UpdateMealCategory(short id, MealCategoryUpdateRequest mealCategory);
        void DeleteMealCategory(short id);
    }
}
