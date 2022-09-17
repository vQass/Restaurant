using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.MealModels
{
    public class MealGroupViewModel
    {
        public string GroupName { get; set; }
        public List<MealViewModel> Meals { get; set; }
    }
}
