using Restaurant.Data.Models.IngredientModels;
using Restaurant.DB.Entities;

namespace Restaurant.IServices
{
    public interface IIngredientService
    {
        public Task<IEnumerable<Ingredient>> GetIngredients();
        public Task<IngredientAdminPanelWrapper> GetIngredientsForAdminPanel(int pageIndex, int pageSize);
        public Ingredient GetIngredient(int id);
        public int AddIngredient(string ingredientName);
        public void UpdateIngredient(int id, string ingredientName);
        public void DeleteIngredient(int id);
    }
}
