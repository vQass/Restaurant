using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Restaurant.DB.Entities
{
    public class MealCategory
    {
        [Key]
        public short Id { get; set; }
        [MaxLength(127)]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual List<Meal>? Meals { get; set; }
    }
}
