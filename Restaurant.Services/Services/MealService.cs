using AutoMapper;
using Restaurant.Business.IRepositories;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.IngredientModels;
using Restaurant.Data.Models.MealModels;

namespace Restaurant.Business.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMapper _mapper;

        public MealService(
            IMealRepository mealRepository,
            IMapper mapper)
        {
            _mealRepository = mealRepository;
            _mapper = mapper;
        }

        public MealViewModel GetMeal(int id)
        {
            var meal = _mealRepository.GetMeal(id);

            var mealVM = _mapper.Map<MealViewModel>(meal);

            return mealVM;
        }

        public async Task<MealWrapper> GetMealPage(int pageIndex, int pageSize)
        {
            var meals = await _mealRepository.GetMeals(pageIndex, pageSize);

            var mealsVM = _mapper.Map<List<MealViewModel>>(meals);
            var mealsCount = _mealRepository.GetMealsCount();

            var wrapper = new MealWrapper
            {
                Items = mealsVM,
                ItemCount = mealsCount
            };

            return wrapper;
        }

        public async Task<IEnumerable<MealGroupViewModel>> GetActiveMealsGroupedByCategory()
        {
            var mealCategories = await _mealRepository.GetMealsGroupedByCategory();

            var mealGroups = mealCategories.Select(x => new MealGroupViewModel()
            {
                GroupName = x.Name,
                Meals = x.Meals
                .Where(x => x.Available)
                .Select(y => new MealGroupItemViewModel()
                {
                    Id = y.Id,
                    Name = y.Name,
                    Price = y.Price,
                    Ingredients = _mapper.Map<List<IngredientViewModel>>(y.Ingredients)
                }).ToList()
            });

            return mealGroups;
        }

        public void AddMeal(MealCreateRequest mealCreateRequest)
        {
            _mealRepository.EnsureMealNameNotTaken(mealCreateRequest.Name);

            _mealRepository.AddMeal(mealCreateRequest);
        }

        public void UpdateMeal(int id, MealUpdateRequest mealUpdateRequest)
        {
            var meal = _mealRepository.GetMeal(id);

            _mealRepository.EnsureMealExists(meal);

            _mealRepository.EnsureMealNameNotTaken(mealUpdateRequest.Name, id);

            _mealRepository.EnsureMealNotInUse(meal);

            _mealRepository.UpdateMeal(meal, mealUpdateRequest);
        }

        public void UpdateMealsPrice(int id, decimal newPrice)
        {
            var meal = _mealRepository.GetMeal(id);

            _mealRepository.EnsureMealExists(meal);

            _mealRepository.UpdateMealsPrice(meal, newPrice);
        }

        public void DeleteMeal(int id)
        {
            var meal = _mealRepository.GetMeal(id);

            _mealRepository.EnsureMealExists(meal);

            _mealRepository.EnsureMealNotInUse(meal);

            _mealRepository.DeleteMeal(meal);
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
    }
}
