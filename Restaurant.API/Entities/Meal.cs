using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.API.Entities
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
        public bool Available { get; set; } = true;
    }
}
