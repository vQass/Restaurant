using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.OrderModels
{
    public class OrderElementCreateRequest
    {
        public int MealId { get; set; }
        public short Amount { get; set; }
    }
}
