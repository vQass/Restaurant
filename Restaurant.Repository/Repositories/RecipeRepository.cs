using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.DB.Entities;
using Restaurant.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Data.Models.RecipeModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.IRepository;

namespace Restaurant.Repository.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<RecipeRepository> _logger;
        private readonly IMapper _mapper;

        public RecipeRepository(
            RestaurantDbContext dbContext,
            ILogger<RecipeRepository> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public Recipe GetRecipe(int mealId)
        {
            var mealWithRecipe = _dbContext.Meals
                .Include(x => x.Ingredients)
                .FirstOrDefault(x => x.Id == mealId);

            var recipe = _mapper.Map<Recipe>(mealWithRecipe);

            return recipe;
        }
    }
}
