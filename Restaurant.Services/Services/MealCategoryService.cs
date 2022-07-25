using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IServices.Interfaces;

namespace Restaurant.Services.Services
{
    public class MealCategoryService : IMealCategoryService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<MealCategoryService> _logger;

        public MealCategoryService(
            RestaurantDbContext dbContext,
            IMapper mapper,
            ILogger<MealCategoryService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public List<MealCategory> GetAllMealsCategories()
        {
            var mealCategories = _dbContext.MealsCategories.ToList();

            return mealCategories;
        }
        public short AddMealCategory(MealCategoryCreateRequest mealCategoryCreateRequest)
        {
            var mealCategory = _mapper.Map<MealCategory>(mealCategoryCreateRequest);

            _dbContext.MealsCategories.Add(mealCategory);
            _dbContext.SaveChanges();

            return mealCategory.Id;
        }
        public void UpdateMealCategory(short id, MealCategoryUpdateRequest mealCategoryUpdateRequest)
        {
            var mealCategory = CheckIfMealCategoryExists(id);

            CheckIfMealCategoryUsedInAnyMeal(mealCategory);

            mealCategory.Name = mealCategoryUpdateRequest.Name;
            _dbContext.SaveChanges();
        }

        public void DeleteMealCategory(short id)
        {
            var mealCategory = CheckIfMealCategoryExists(id);

            CheckIfMealCategoryUsedInAnyMeal(mealCategory);

            _dbContext.MealsCategories.Remove(mealCategory);
            _dbContext.SaveChanges();
        }

        private MealCategory CheckIfMealCategoryExists(short id)
        {
            var mealCategory = _dbContext.MealsCategories.FirstOrDefault(x => x.Id == id);

            if (mealCategory == null)
            {
                _logger.LogError($"Meal category with id:{id} does not exist.");
                throw new NotFoundException("Kategoria dań o podanym id nie istnieje.");
            }

            return mealCategory;
        }

        private void CheckIfMealCategoryUsedInAnyMeal(MealCategory mealCategory)
        {
            if (_dbContext.Meals.Any(x => x.MealCategory == mealCategory))
            {
                _logger.LogError($"Selected category with id:{mealCategory.Id} is used in at least one meal.");
                throw new BadRequestException("Wybrana kategoria przypisana jest do co najmniej jednego dania.");
            }
        }
    }
}
