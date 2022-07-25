using Restaurant.Data.Models.MealModels;
using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.IServices.Interfaces
{
    public interface IMealService
    {
        public int AddMeal(MealCreateRequest mealCreateRequest);
        public void UpdateMeal(int id, MealUpdateRequest mealUpdateRequest);
        public void DeleteMeal(int id);
        public void SetMealAsUnavailable(int id);
        public void SetMealAsAvailable(int id);
        public void UpdateMealsPrice(int id, decimal newPrice);
        public IEnumerable<Meal> GetAllMeals();
    }
}
