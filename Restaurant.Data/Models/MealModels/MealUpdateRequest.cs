using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.MealModels
{
    public class MealUpdateRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte CategoryId { get; set; }
    }
}
