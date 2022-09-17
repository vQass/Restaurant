using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.IngredientModels;
using Restaurant.Data.Models.MealModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;
using Restaurant.IRepository;
using Restaurant.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Services
{
    public class MealService : IMealService
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMealRepository _mealRepository;

        #endregion Fields

        #region Ctors

        public MealService(
            IMapper mapper,
            IMealRepository mealRepository)
        {
            _mapper = mapper;
            _mealRepository = mealRepository;
        }

        #endregion Ctors

        #region PublicMethods

        public async Task<IEnumerable<Meal>> GetMeals()
        {
            return await _mealRepository.GetMeals();
        }

        public async Task<IEnumerable<MealGroupViewModel>> GetMealsGroupedByCategory()
        {
            var mealCategories = await _mealRepository.GetMealsGroupedByCategory();

            var mealGroups = mealCategories.Select(x => new MealGroupViewModel()
            {
                GroupName = x.Name,
                Meals = x.Meals.Select(y => new MealViewModel()
                {
                    Name = y.Name,
                    Price = y.Price,
                    Ingredients = y.RecipeElements.Select(z => new IngredientViewModel()
                    {
                        Name = z.Ingredient.Name
                    }).ToList()
                }).ToList()
            });

            return mealGroups;
        }

        public int AddMeal(MealCreateRequest mealCreateRequest)
        {
            _mealRepository.EnsureMealNameNotTaken(mealCreateRequest.Name);

            var id = _mealRepository.AddMeal(mealCreateRequest);

            return id;
        }

        public void DeleteMeal(int id)
        {
            var meal = _mealRepository.GetMeal(id);

            _mealRepository.EnsureMealExists(meal);

            _mealRepository.EnsureMealNotInUse(meal);

            _mealRepository.DeleteMeal(meal);
        }

        public void UpdateMeal(int id, MealUpdateRequest mealUpdateRequest)
        {
            var meal = _mealRepository.GetMeal(id);

            _mealRepository.EnsureMealExists(meal);

            _mealRepository.EnsureMealNameNotTaken(mealUpdateRequest.Name, id);

            _mealRepository.EnsureMealNotInUse(meal);

            _mealRepository.UpdateMeal(meal, mealUpdateRequest);
        }

        public void SetMealAsUnavailable(int id)
        {
            var meal = _mealRepository.GetMeal(id);

            _mealRepository.EnsureMealExists(meal);

            _mealRepository.SetMealAsUnavailable(meal);
        }

        public void SetMealAsAvailable(int id)
        {
            var meal = _mealRepository.GetMeal(id);

            _mealRepository.EnsureMealExists(meal);

            _mealRepository.SetMealAsAvailable(meal);
        }

        public void UpdateMealsPrice(int id, decimal newPrice)
        {
            var meal = _mealRepository.GetMeal(id);

            _mealRepository.EnsureMealExists(meal);

            _mealRepository.UpdateMealsPrice(meal, newPrice);
        }

        #endregion PublicMethods
    }
}
