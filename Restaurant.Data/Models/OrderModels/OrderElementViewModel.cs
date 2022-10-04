using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.OrderModels
{
    public class OrderElementViewModel
    {
        public string MealName { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
