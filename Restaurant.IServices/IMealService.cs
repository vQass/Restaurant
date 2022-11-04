using Restaurant.Data.Models.MealModels;
using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.IServices
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetMeals();
        Task<MealAdminPanelWrapper> GetMealsForAdminPanel();
        Task<IEnumerable<MealGroupViewModel>> GetMealsGroupedByCategory();

        int AddMeal(MealCreateRequest mealCreateRequest);
        void UpdateMeal(int id, MealUpdateRequest mealUpdateRequest);
        void DeleteMeal(int id);
        void SetMealAsUnavailable(int id);
        void SetMealAsAvailable(int id);
        void UpdateMealsPrice(int id, decimal newPrice);

    }
}
