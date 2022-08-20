using Microsoft.EntityFrameworkCore;
using Restaurant.DB.Entities;
using Restaurant.IRepository;
using Restaurant.IServices;

namespace Restaurant.Services.Services
{
    public class IngredientService : IIngredientService
    {
        #region Fields

        private readonly IIngredientRepository _ingredientRepository;

        #endregion

        #region Ctors

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        #endregion

        #region PublicMethods

        public Ingredient GetIngredientById(int id)
        {
            var ingredient = _ingredientRepository.GetIngredient(id);

            _ingredientRepository.EnsureIngredientExists(ingredient);

            return ingredient;
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsList()
        {
            return await _ingredientRepository.GetIngredients();
        }

        public int AddIngredient(string ingredientName)
        {
            var id = _ingredientRepository
                .AddIngredient(new Ingredient { Name = ingredientName } );

            return id;
        }

        public void DeleteIngredient(int id)
        {
            var ingredient = _ingredientRepository.GetIngredient(id);

            _ingredientRepository.EnsureIngredientExists(ingredient);

            _ingredientRepository.DeleteIngredient(ingredient);
        }

        public void UpdateIngredient(int id, string ingredientName)
        {
            var ingredient = _ingredientRepository.GetIngredient(id);

            _ingredientRepository.EnsureIngredientExists(ingredient);

            _ingredientRepository.EnsureIngredientNameNotTaken(ingredientName, id);

            _ingredientRepository.UpdateIngredient(ingredient, ingredientName);
        }

        #endregion
    }
}
