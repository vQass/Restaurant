using Restaurant.Business.IServices;
using Restaurant.Data.Models.IngredientModels;
using Restaurant.DB.Entities;
using Restaurant.IRepository;

namespace Restaurant.Business.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        public async Task<IngredientAdminPanelWrapper> GetIngredientsForAdminPanel(int pageIndex, int pageSize)
        {
            var items = await _ingredientRepository.GetIngredients(pageIndex, pageSize);
            var itemsCount = _ingredientRepository.GetIngredientsCount();

            return new IngredientAdminPanelWrapper
            {
                Items = items.Select(x => new IngredientAdminPanelItem
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList(),
                ItemsCount = itemsCount
            };
        }

        public Ingredient GetIngredient(int id)
        {
            var ingredient = _ingredientRepository.GetIngredient(id);

            _ingredientRepository.EnsureIngredientExists(ingredient);

            return ingredient;
        }

        public async Task<IEnumerable<Ingredient>> GetIngredients()
        {
            return await _ingredientRepository.GetIngredients(0, 0);
        }

        public int AddIngredient(string ingredientName)
        {
            _ingredientRepository.EnsureIngredientNameNotTaken(ingredientName);

            var id = _ingredientRepository
                .AddIngredient(new Ingredient { Name = ingredientName.Trim() });

            return id;
        }

        public void DeleteIngredient(int id)
        {
            var ingredient = _ingredientRepository.GetIngredient(id);

            _ingredientRepository.EnsureIngredientExists(ingredient);

            _ingredientRepository.EnsureIngredientNotInUse(ingredient);

            _ingredientRepository.DeleteIngredient(ingredient);
        }

        public void UpdateIngredient(int id, string ingredientName)
        {
            var ingredient = _ingredientRepository.GetIngredient(id);

            _ingredientRepository.EnsureIngredientExists(ingredient);

            _ingredientRepository.EnsureIngredientNotInUse(ingredient);

            _ingredientRepository.EnsureIngredientNameNotTaken(ingredientName, id);

            _ingredientRepository.UpdateIngredient(ingredient, ingredientName.Trim());
        }
    }
}
