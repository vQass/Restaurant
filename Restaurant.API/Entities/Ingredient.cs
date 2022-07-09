using System.ComponentModel.DataAnnotations;

namespace Restaurant.API.Entities
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(127)]
        public string Name { get; set; }
    }
}
