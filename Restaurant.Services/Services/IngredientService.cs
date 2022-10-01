using Microsoft.EntityFrameworkCore;
using Restaurant.DB.Entities;
using Restaurant.IRepository;
using Restaurant.IServices;

namespace Restaurant.Services.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }


        public Ingredient GetIngredient(int id)
        {
            var ingredient = _ingredientRepository.GetIngredient(id);

            _ingredientRepository.EnsureIngredientExists(ingredient);

            return ingredient;
        }

        public async Task<IEnumerable<Ingredient>> GetIngredients()
        {
            return await _ingredientRepository.GetIngredients();
        }

        public int AddIngredient(string ingredientName)
        {
            _ingredientRepository.EnsureIngredientNameNotTaken(ingredientName);

            var id = _ingredientRepository
                .AddIngredient(new Ingredient { Name = ingredientName.Trim() } );

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

            _ingredientRepository.UpdateIngredient(ingredient, ingredientName.Trim());
        }
    }
}
