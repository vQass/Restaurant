using AutoMapper;
using Restaurant.Business.IRepositories;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.DB.Entities;

namespace Restaurant.Business.Services
{
    public class MealCategoryService : IMealCategoryService
    {
        private readonly IMealCategoryRepository _mealCategoryRepository;
        private readonly IMapper _mapper;

        public MealCategoryService(IMealCategoryRepository mealCategoryRepository, IMapper mapper)
        {
            _mealCategoryRepository = mealCategoryRepository;
            _mapper = mapper;
        }

        public MealCategoryViewModel GetMealCategory(short id)
        {
            var category = _mealCategoryRepository.GetMealCategory(id);

            return _mapper.Map<MealCategoryViewModel>(category);
        }

        public IEnumerable<MealCategory> GetMealCategories()
        {
            return _mealCategoryRepository.GetMealCategories();
        }

        public short AddMealCategory(string mealCategoryName)
        {
            _mealCategoryRepository.EnsureMealCategoryNameNotTaken(mealCategoryName);

            var id = _mealCategoryRepository.AddMealCategory(new MealCategory() { Name = mealCategoryName });

            return id;
        }

        public void UpdateMealCategory(short id, string mealCategoryName)
        {
            var mealCategory = _mealCategoryRepository.GetMealCategory(id);

            _mealCategoryRepository.EnsureMealCategoryExists(mealCategory);

            _mealCategoryRepository.EnsureMealCategoryNameNotTaken(mealCategoryName, id);

            _mealCategoryRepository.EnsureMealCategoryNotInUse(mealCategory);

            _mealCategoryRepository.UpdateMealCategory(mealCategory, mealCategoryName);
        }

        public void DeleteMealCategory(short id)
        {
            var mealCategory = _mealCategoryRepository.GetMealCategory(id);

            _mealCategoryRepository.EnsureMealCategoryExists(mealCategory);

            _mealCategoryRepository.EnsureMealCategoryNotInUse(mealCategory);

            _mealCategoryRepository.DeleteMealCategory(mealCategory);
        }

        public MealCategoryWrapper GetMealCategoriesPage(int pageIndex, int pageSize)
        {
            var mealCategories = _mealCategoryRepository.GetMealCategories(pageIndex, pageSize);
            var itemCount = _mealCategoryRepository.GetMealCategoriesCount();

            var mealCategoriesVM = _mapper.Map<List<MealCategoryViewModel>>(mealCategories);

            return new MealCategoryWrapper
            {
                Items = mealCategoriesVM,
                ItemCount = itemCount
            };
        }

    }
}
