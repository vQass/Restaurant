using AutoMapper;
using Restaurant.Business.IRepositories;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.IngredientModels;

namespace Restaurant.Business.Services
{
    internal class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public IngredientViewModel GetIngredient(int id)
        {
            var ingredient = _ingredientRepository.GetIngredient(id);
            _ingredientRepository.EnsureIngredientExists(ingredient);

            var ingredientVM = _mapper.Map<IngredientViewModel>(ingredient);

            return ingredientVM;
        }

        public async Task<IngredientWrapper> GetIngredientPage(int pageIndex, int pageSize)
        {
            var ingredients = await _ingredientRepository.GetIngredients(pageIndex, pageSize);
            var itemsCount = _ingredientRepository.GetIngredientsCount();

            var ingredientsVM = _mapper.Map<List<IngredientViewModel>>(ingredients);

            return new IngredientWrapper
            {
                Items = ingredientsVM,
                ItemsCount = itemsCount
            };
        }

        public async Task<IEnumerable<IngredientViewModel>> GetIngredients()
        {
            var ingredients = await _ingredientRepository.GetIngredients();
            var ingredientsVM = _mapper.Map<List<IngredientViewModel>>(ingredients);

            return ingredientsVM;
        }

        public void AddIngredient(IngredientCreateRequest ingredientRequest)
        {
            _ingredientRepository.EnsureIngredientNameNotTaken(ingredientRequest.Name);

            _ingredientRepository.AddIngredient(ingredientRequest);
        }

        public void UpdateIngredient(int id, IngredientUpdateRequest ingredientRequest)
        {
            var ingredient = _ingredientRepository.GetIngredient(id);

            _ingredientRepository.EnsureIngredientExists(ingredient);

            _ingredientRepository.EnsureIngredientNotInUse(ingredient);

            _ingredientRepository.EnsureIngredientNameNotTaken(ingredientRequest.Name, id);

            _ingredientRepository.UpdateIngredient(ingredient, ingredientRequest);
        }

        public void DeleteIngredient(int id)
        {
            var ingredient = _ingredientRepository.GetIngredient(id);

            _ingredientRepository.EnsureIngredientExists(ingredient);

            _ingredientRepository.EnsureIngredientNotInUse(ingredient);

            _ingredientRepository.DeleteIngredient(ingredient);
        }
    }
}
