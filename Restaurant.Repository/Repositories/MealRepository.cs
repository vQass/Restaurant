using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.MealModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IRepository;

namespace Restaurant.Repository.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly ILogger<MealRepository> _logger;
        private readonly IMapper _mapper;
        private readonly RestaurantDbContext _dbContext;

        public MealRepository(
            RestaurantDbContext dbContext,
            ILogger<MealRepository> logger,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        #region GetMethods

        public async Task<IEnumerable<Meal>> GetMeals()
        {
            return await _dbContext.Meals
                .Include(x => x.MealCategory)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<MealCategory>> GetMealsGroupedByCategory()
        {
            return await _dbContext.MealsCategories
                .Include(x => x.Meals)
                .ThenInclude(x => x.Ingredients)
                .ToListAsync();
        }

        public Meal GetMeal(int id)
        {
            return _dbContext.Meals
                .Include(x => x.MealCategory)
                .FirstOrDefault(x => x.Id == id);
        }

        public int GetMealsCount()
        {
            return _dbContext.Meals.Count();
        }

        #endregion

        #region EntityModificationMethods

        public int AddMeal(MealCreateRequest mealCreateRequest)
        {
            var meal = _mapper.Map<Meal>(mealCreateRequest);

            int price = (int)(meal.Price * 100);
            meal.Price = (decimal)(price / 100.0);

            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();

            return meal.Id;
        }

        public void DeleteMeal(Meal meal)
        {
            _dbContext.Meals.Remove(meal);
            _dbContext.SaveChanges();
        }

        public void UpdateMeal(Meal meal, MealUpdateRequest mealUpdateRequest)
        {
            meal.Name = mealUpdateRequest.Name;

            int price = (int)(mealUpdateRequest.Price * 100);
            meal.Price = (decimal)(price / 100.0);

            meal.MealCategory = _dbContext.MealsCategories
                .FirstOrDefault(x => x.Id == mealUpdateRequest.MealCategoryId);

            _dbContext.SaveChanges();
        }

        public void SetMealAsUnavailable(Meal meal)
        {
            meal.Available = false;
            _dbContext.SaveChanges();
        }

        public void SetMealAsAvailable(Meal meal)
        {
            meal.Available = true;
            _dbContext.SaveChanges();
        }

        public void UpdateMealsPrice(Meal meal, decimal newPrice)
        {
            int price = (int)(newPrice * 100);
            meal.Price = (decimal)(price / 100.0);

            _dbContext.SaveChanges();
        }

        #endregion

        #region ValidationMethods

        public void EnsureMealExists(Meal meal)
        {
            if (meal is null)
            {
                throw new NotFoundException("Podane danie nie istnieje.");
            }
        }

        public void EnsureMealNameNotTaken(string mealName, int id = 0)
        {
            var mealNameTaken = _dbContext.Meals
                .Where(x => x.Id != id)
                .Any(x => x.Name
                    .ToLower()
                    .Replace(" ", "")
                    .Equals(
                        mealName
                            .ToLower()
                            .Replace(" ", "")));

            if (mealNameTaken)
            {
                _logger.LogError($"Meal name: {mealName} is taken.");
                throw new NotFoundException("Podana nazwa dania jest zajęta.");
            }
        }

        public void EnsureMealNotInUse(Meal meal)
        {
            var mealInUse = _dbContext.OrdersElements
                .Any(x => x.MealId == meal.Id);

            if (mealInUse)
            {
                _logger.LogError($"Meal with name {meal.Name} is used in orders elements table.");
                throw new BadRequestException("Podane danie używane jest w co najmniej jednym zamówieniu.");
            }
        }

        #endregion
    }
}
