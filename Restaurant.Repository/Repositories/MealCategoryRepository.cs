using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IRepository;
using Restaurant.LinqHelpers.Helpers;

namespace Restaurant.Repository.Repositories
{
    public class MealCategoryRepository : IMealCategoryRepository
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<MealCategoryRepository> _logger;

        public MealCategoryRepository(
            RestaurantDbContext dbContext,
            ILogger<MealCategoryRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region GetMethods

        public IEnumerable<MealCategory> GetMealCategories(int pageIndex = 0, int pageSize = 0)
        {
            return _dbContext.MealsCategories
                .AsQueryable()
                .ApplyPaging(pageIndex, pageSize)
                .ToList();
        }

        public MealCategory GetMealCategory(short id)
        {
            return _dbContext.MealsCategories.FirstOrDefault(x => x.Id == id);
        }

        public int GetMealCategoriesCount()
        {
            return _dbContext.MealsCategories.Count();
        }

        #endregion

        #region EntityModificationMethods

        public short AddMealCategory(MealCategory mealCategory)
        {
            mealCategory.Name = mealCategory.Name;

            _dbContext.MealsCategories.Add(mealCategory);
            _dbContext.SaveChanges();
            return mealCategory.Id;
        }

        public void UpdateMealCategory(MealCategory mealCategory, string newMealCategoryName)
        {
            mealCategory.Name = newMealCategoryName;
            _dbContext.SaveChanges();
        }

        public void DeleteMealCategory(MealCategory mealCategory)
        {
            _dbContext.MealsCategories.Remove(mealCategory);
            _dbContext.SaveChanges();
        }

        #endregion

        #region ValidationMethods

        public void EnsureMealCategoryExists(MealCategory mealCategory)
        {
            if (mealCategory == null)
            {
                throw new NotFoundException("Podana kategoria dań nie istnieje.");
            }
        }

        public void EnsureMealCategoryNameNotTaken(string mealCategoryName, int id = 0)
        {
            var mealCategoryNameTaken = _dbContext.MealsCategories
                .Where(x => x.Id != id)
                .Any(x => x.Name
                    .ToLower()
                    .Replace(" ", "")
                    .Equals(
                        mealCategoryName
                            .ToLower()
                            .Replace(" ", "")));

            if (mealCategoryNameTaken)
            {
                _logger.LogError($"Name: {mealCategoryName} is taken.");
                throw new BadRequestException("Podana nazwa kategorii dań jest zajęta.");
            }
        }

        public void EnsureMealCategoryNotInUse(MealCategory mealCategory)
        {
            var mealCategoryInUse = _dbContext.Meals.Any(x => x.MealCategoryId == mealCategory.Id);

            if (mealCategoryInUse)
            {
                _logger.LogError($"Selected category with name: {mealCategory.Name} is used in at least one meal.");
                throw new BadRequestException("Wybrana kategoria dań przypisana jest do co najmniej jednego dania.");
            }
        }

        #endregion
    }
}
