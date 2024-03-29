﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Business.IRepositories;
using Restaurant.Data.Exceptions;
using Restaurant.Data.Models.IngredientModels;
using Restaurant.DB;
using Restaurant.Entities.Entities;
using Restaurant.LinqHelpers.Helpers;

namespace Restaurant.Repository.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<IngredientRepository> _logger;

        public IngredientRepository(
            RestaurantDbContext dbContext,
            ILogger<IngredientRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region GetMethods

        public Ingredient GetIngredient(int id)
        {
            var ingredient = _dbContext.Ingredients
                .FirstOrDefault(x => x.Id == id);

            return ingredient;
        }

        public Ingredient GetIngredient(string ingredientName)
        {
            var ingredient = _dbContext.Ingredients
                .FirstOrDefault(x => x.Name == ingredientName);

            return ingredient;
        }

        public async Task<IEnumerable<Ingredient>> GetIngredients(int pageIndex = 0, int pageSize = 0)
        {
            return await _dbContext.Ingredients
                .ApplyPaging(pageIndex, pageSize)
                .ToListAsync();
        }

        public IEnumerable<Ingredient> GetIngredients(List<int> ids)
        {
            return _dbContext.Ingredients
                .Where(x => ids.Contains(x.Id))
                .ToList();
        }

        public int GetIngredientsCount()
        {
            return _dbContext.Ingredients.Count();
        }

        #endregion

        #region EntityModificationMethods

        public void AddIngredient(IngredientCreateRequest ingredientRequest)
        {
            var ingredient = new Ingredient
            {
                Name = ingredientRequest.Name.Trim()
            };

            _dbContext.Ingredients.Add(ingredient);
            _dbContext.SaveChanges();
        }

        public void UpdateIngredient(Ingredient ingredient, IngredientUpdateRequest ingredientRequest)
        {
            ingredient.Name = ingredientRequest.Name.Trim();
            _dbContext.SaveChanges();
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            _dbContext.Ingredients.Remove(ingredient);
            _dbContext.SaveChanges();
        }

        #endregion

        #region ValidationMethods

        public void EnsureIngredientExists(Ingredient ingredient)
        {
            if (ingredient is null)
            {
                throw new NotFoundException("Podany składnik nie istnieje.");
            }
        }

        public void EnsureIngredientNameNotTaken(string ingredientName, int id = 0)
        {
            var ingredientNameTaken = _dbContext.Ingredients
                .Where(x => x.Id != id)
                .Any(x => x.Name
                    .ToLower()
                    .Replace(" ", "")
                    .Equals(
                        ingredientName
                        .ToLower()
                        .Replace(" ", "")));

            if (ingredientNameTaken)
            {
                _logger.LogError($"Ingredient name: {ingredientName} is taken.");
                throw new BadRequestException("Podana nazwa składnik jest zajęta.");
            }
        }

        public void EnsureIngredientNotInUse(Ingredient ingredient)
        {
            var ingredientInUse = _dbContext.Meals
                .Any(x => x.Ingredients.Contains(ingredient));

            if (ingredientInUse)
            {
                _logger.LogError($"Ingredient with name {ingredient.Name} is used in recipes table.");
                throw new BadRequestException("Podany składnik używany jest w co najmniej jednym przepisie.");
            }
        }

        #endregion
    }
}
