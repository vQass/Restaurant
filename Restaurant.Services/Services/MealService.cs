using AutoMapper;
using Restaurant.Data.Models.IngredientModels;
using Restaurant.Data.Models.MealModels;
using Restaurant.Data.Models.RecipeModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IRepository;
using Restaurant.IServices;


namespace Restaurant.Services.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMealCategoryRepository _mealCategoryRepository;

        public MealService(
            IMealRepository mealRepository,
            IMealCategoryRepository mealCategoryRepository,
            IRecipeRepository recipeRepository,
            RestaurantDbContext dbContext)
        {
            _mealRepository = mealRepository;
            _mealCategoryRepository = mealCategoryRepository;
        }


        public async Task<IEnumerable<Meal>> GetMeals()
        {
            return await _mealRepository.GetMeals();
        }
        
        public async Task<MealAdminPanelWrapper> GetMealsForAdminPanel(int pageIndex, int pageSize)
        {
            var meals = await _mealRepository.GetMeals(pageIndex, pageSize);

            var mealCategories = _mealCategoryRepository.GetMealCategories();

            var mealAdminPanelItems = meals.Select(x => new MealAdminPanelItem
            {
                Id = x.Id,
                Name = x.Name,
                Available = x.Available,
                Price = x.Price,
                MealCategoryId = x.MealCategoryId, 
                MealCategoryName = mealCategories.FirstOrDefault(y => y.Id == x.MealCategoryId).Name
            });

            var mealsCount = _mealRepository.GetMealsCount();

            var mealWrapper = new MealAdminPanelWrapper
            {
                ItemCount = mealsCount,
                Items = mealAdminPanelItems.ToList()
            };

            return mealWrapper;
        }

        public MealAdminPanelItem GetMealForAdminPanel(int id)
        {
            var meal = _mealRepository.GetMeal(id);

            var mealCategory = _mealCategoryRepository.GetMealCategory(meal.MealCategoryId);

            var mealAdminPanelItem = new MealAdminPanelItem
            {
                Id = meal.Id,
                Name = meal.Name,
                Price = meal.Price,
                Available = meal.Available,
                MealCategoryId = meal.MealCategoryId,
                MealCategoryName = mealCategory.Name
            };

            return mealAdminPanelItem;
        }

        public async Task<IEnumerable<MealGroupViewModel>> GetActiveMealsGroupedByCategory()
        {
            var mealCategories = await _mealRepository.GetMealsGroupedByCategory();

            var mealGroups = mealCategories.Select(x => new MealGroupViewModel()
            {
                GroupName = x.Name,
                Meals = x.Meals
                .Where(x => x.Available)
                .Select(y => new MealViewModel()
                {
                    Id = y.Id,
                    Name = y.Name,
                    Price = y.Price,
                    Ingredients = y.Ingredients.Select(z => new IngredientViewModel()
                    {
                        Name = z.Name
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
    }
}
