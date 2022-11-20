using Restaurant.DB.Entities;

namespace Restaurant.IRepository
{
    public interface IIngredientRepository
    {
        Ingredient GetIngredient(int id);
        Ingredient GetIngredient(string ingredientName);
        Task<IEnumerable<Ingredient>> GetIngredients(int pageIngex, int pageSize);
        IEnumerable<Ingredient> GetIngredients(List<int> ids);
        int GetIngredientsCount();

        int AddIngredient(Ingredient ingredient);
        void DeleteIngredient(Ingredient ingredient);

        void EnsureIngredientExists(Ingredient ingredient);
        void EnsureIngredientNameNotTaken(string ingredientName, int id = 0);
        void EnsureIngredientNotInUse(Ingredient ingredient);
        void UpdateIngredient(Ingredient ingredient, string newIngredientName);
    }
}