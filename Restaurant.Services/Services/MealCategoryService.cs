using Restaurant.DB.Entities;
using Restaurant.IRepository;
using Restaurant.IServices;

namespace Restaurant.Services.Services
{
    public class MealCategoryService : IMealCategoryService
    {
        private readonly IMealCategoryRepository _mealCategoryRepository;

        public MealCategoryService(IMealCategoryRepository mealCategoryRepository)
        {
            _mealCategoryRepository = mealCategoryRepository;
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
    }
}
