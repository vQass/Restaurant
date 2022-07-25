using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.DB.Entities
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(127)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public bool Available { get; set; } = false;

        public short CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual MealCategory MealCategory { get; set; }
        public virtual List<Recipe> Recipe { get; set; }
    }
}
