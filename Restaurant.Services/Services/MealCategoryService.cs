using AutoMapper;
using Restaurant.Business.IRepositories;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.MealCategoryModels;

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

        public List<MealCategoryViewModel> GetMealCategories()
        {
            var mealCategories = _mealCategoryRepository.GetMealCategories();
            var mealCategoriesVM = _mapper.Map<List<MealCategoryViewModel>>(mealCategories);

            return mealCategoriesVM;
        }

        public MealCategoryWrapper GetMealCategoriesPage(int pageIndex, int pageSize)
        {
            var mealCategories = _mealCategoryRepository.GetMealCategories(pageIndex, pageSize);
            var mealCategoriesVM = _mapper.Map<List<MealCategoryViewModel>>(mealCategories);
            var mealCateogiesCount = _mealCategoryRepository.GetMealCategoriesCount();

            var wrapper = new MealCategoryWrapper
            {
                Items = mealCategoriesVM,
                ItemCount = mealCateogiesCount
            };

            return wrapper;
        }

        public void AddMealCategory(MealCategoryCreateRequest mealCategoryRequest)
        {
            _mealCategoryRepository.EnsureMealCategoryNameNotTaken(mealCategoryRequest.Name);

            _mealCategoryRepository.AddMealCategory(mealCategoryRequest);
        }

        public void UpdateMealCategory(short id, MealCategoryUpdateRequest mealCategoryRequest)
        {
            var mealCategory = _mealCategoryRepository.GetMealCategory(id);

            _mealCategoryRepository.EnsureMealCategoryExists(mealCategory);

            _mealCategoryRepository.EnsureMealCategoryNameNotTaken(mealCategoryRequest.Name, id);

            _mealCategoryRepository.EnsureMealCategoryNotInUse(mealCategory);

            _mealCategoryRepository.UpdateMealCategory(mealCategory, mealCategoryRequest);
        }

        public void DeleteMealCategory(short id)
        {
            var mealCategory = _mealCategoryRepository.GetMealCategory(id);

            _mealCategoryRepository.EnsureMealCategoryExists(mealCategory);

            _mealCategoryRepository.EnsureMealCategoryNotInUse(mealCategory);

            _mealCategoryRepository.DeleteMealCategory(mealCategory);
        }
    }
}
