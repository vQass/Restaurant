using Restaurant.Data.Models.IngredientModels;
using Restaurant.Entities.Entities;

namespace Restaurant.Business.IRepositories
{
    public interface IIngredientRepository
    {
        Ingredient GetIngredient(int id);
        Ingredient GetIngredient(string ingredientName);
        Task<IEnumerable<Ingredient>> GetIngredients(int pageIngex = 0, int pageSize = 0);
        IEnumerable<Ingredient> GetIngredients(List<int> ids);
        int GetIngredientsCount();

        void AddIngredient(IngredientCreateRequest ingredientRequest);
        void UpdateIngredient(Ingredient ingredient, IngredientUpdateRequest ingredientRequest);
        void DeleteIngredient(Ingredient ingredient);

        void EnsureIngredientExists(Ingredient ingredient);
        void EnsureIngredientNameNotTaken(string ingredientName, int id = 0);
        void EnsureIngredientNotInUse(Ingredient ingredient);
    }
}