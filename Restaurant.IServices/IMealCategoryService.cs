using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
