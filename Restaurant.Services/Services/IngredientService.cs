using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IServices;

namespace Restaurant.Services.Services
{
    public class IngredientService : IIngredientService
    {
        #region Fields

        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<IngredientService> _logger;

        #endregion

        #region Ctors

        public IngredientService(
            RestaurantDbContext dbContext,
            ILogger<IngredientService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #endregion

        #region PublicMethods

        public int AddIngredient(string ingredientName)
        {
            CheckIfIngredientNameInUse(ingredientName);

            var ingredient = new Ingredient();
            ingredient.Name = ingredientName;

            _dbContext.Ingredients.Add(ingredient);
            _dbContext.SaveChanges();

            return ingredient.Id;
        }

        public void DeleteIngredient(int id)
        {
            var ingredient = CheckIfIngredientExistsById(id);

            CheckIfIngredientInUse(ingredient);

            _dbContext.Ingredients.Remove(ingredient);
            _dbContext.SaveChanges();
        }

        public Ingredient GetIngredientById(int id)
        {
            return CheckIfIngredientExistsById(id);
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsList()
        {
            return await _dbContext.Ingredients.ToListAsync();
        }

        public void UpdateIngredient(int id, string ingredientName)
        {
            var ingredient = CheckIfIngredientExistsById(id);

            CheckIfIngredientNameInUse(ingredientName, id);

            ingredient.Name = ingredientName;
            _dbContext.SaveChanges();
        }

        #endregion

        #region PrivateMethods

        private Ingredient CheckIfIngredientExistsById(int id)
        {
            var ingredient = _dbContext.Ingredients
                .FirstOrDefault(x => x.Id == id);

            if (ingredient is null)
            {
                throw new NotFoundException("Składnik o podanym id nie istnieje.");
            }

            return ingredient;
        }

        private Ingredient CheckIfIngredientExistsByName(string ingredientName)
        {
            var ingredient = _dbContext.Ingredients
                .FirstOrDefault(x => x.Name == ingredientName);

            if (ingredient is null)
            {
                throw new NotFoundException("Skladnik o podanej nazwie nie istnieje.");
            }

            return ingredient;
        }

        private void CheckIfIngredientNameInUse(string cityName, int id = 0)
        {
            var ingredientNameInUse = _dbContext.Ingredients
                .Where(x => x.Id != id)
                .Any(
                    x => x.Name.Trim().ToLower() ==
                    cityName.Trim().ToLower());

            if (ingredientNameInUse)
            {
                _logger.LogError("Given name is taken.");
                throw new NotFoundException("Podana nazwa składnik jest zajęta.");
            }
        }

        private void CheckIfIngredientInUse(Ingredient ingredient)
        {
            var ingredientInUse = _dbContext.Recipes
                .Any(x => x.Ingredient == ingredient);

            if (ingredientInUse)
            {
                _logger.LogError($"Ingredient with name {ingredient.Name} is used in recipes table.");
                throw new BadRequestException("Podany składnik używany jest w co najmniej jednym przepisie.");
            }
        }

        #endregion
    }
}
