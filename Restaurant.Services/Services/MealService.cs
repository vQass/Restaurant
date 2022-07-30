using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.MealModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;
using Restaurant.IServices.Interfaces;
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

        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<MealService> _logger;

        #endregion Fields

        #region Ctors

        public MealService(
            RestaurantDbContext dbContext,
            IMapper mapper,

            ILogger<MealService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion Ctors

        #region PublicMethods

        public IEnumerable<Meal> GetAllMeals()
        {
            var meals = 
                _dbContext.Meals
                    .Include(x => x.MealCategory)
                    .ToList();

            return meals;
        }

        public int AddMeal(MealCreateRequest mealCreateRequest)
        {
            var meal = _mapper.Map<Meal>(mealCreateRequest);

            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();

            return meal.Id;
        }

        public void DeleteMeal(int id)
        {
            var meal = CheckIfMealExists(id);

            CheckIfMealInAnyOrder(meal);

            _dbContext.Meals.Remove(meal);
            _dbContext.SaveChanges();
        }

        public void UpdateMeal(int id, MealUpdateRequest mealUpdateRequest)
        {
            var meal = CheckIfMealExists(id);

            CheckIfMealInAnyOrder(meal);

            meal.Name = mealUpdateRequest.Name;
            meal.Price = mealUpdateRequest.Price;
            meal.MealCategory.Id = mealUpdateRequest.CategoryId;

            _dbContext.SaveChanges();
        }

        public void SetMealAsUnavailable(int id)
        {
            var meal = CheckIfMealExists(id);

            meal.Available = false;
            _dbContext.SaveChanges();
        }

        public void SetMealAsAvailable(int id)
        {
            var meal = CheckIfMealExists(id);

            meal.Available = true;
            _dbContext.SaveChanges();
        }

        public void UpdateMealsPrice(int id, decimal newPrice)
        {
            var meal = CheckIfMealExists(id);

            meal.Price = newPrice;
            _dbContext.SaveChanges();
        }

        #endregion PublicMethods

        #region PrivateMethods

        private Meal CheckIfMealExists(int id)
        {
            var meal = _dbContext.Meals.FirstOrDefault(x => x.Id == id);

            if (meal == null)
            {
                _logger.LogError($"Dish with id:{id} does not exist.");
                throw new NotFoundException("Danie o podanym id nie istnieje.");
            }

            return meal;
        }

        private void CheckIfMealInAnyOrder(Meal meal)
        {
            if (_dbContext.OrdersElements.Any(x => x.Meal == meal))
            {
                _logger.LogError($"Dish with id:{meal.Id} is used in at least one user's order.");
                throw new BadRequestException("Wybrane danie znajduje się w zamówieniu co najmniej jednego użytkownika.");
            }
        }

        #endregion PrivateMethods
    }
}
