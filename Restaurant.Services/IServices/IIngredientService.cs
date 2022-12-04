using Restaurant.Data.Models.IngredientModels;

namespace Restaurant.Business.IServices
{
    public interface IIngredientService
    {
        IngredientViewModel GetIngredient(int id);
        Task<IEnumerable<IngredientViewModel>> GetIngredients();
        Task<IngredientWrapper> GetIngredientPage(int pageIndex, int pageSize);

        void AddIngredient(IngredientCreateRequest ingredient);
        void UpdateIngredient(int id, IngredientUpdateRequest ingredient);
        void DeleteIngredient(int id);
    }
}
