using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.IServices
{
    public interface IIngredientService
    {
        public Task<IEnumerable<Ingredient>> GetIngredientsList();
        public Ingredient GetIngredientById(int id);
        public int AddIngredient(string ingredientName);
        public void UpdateIngredient(int id, string ingredientName);
        public void DeleteIngredient(int id);
    }
}
