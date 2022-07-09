using System.ComponentModel.DataAnnotations;

namespace Restaurant.DB.Entities
{
    public class City
    {
        [Key]
        public short Id { get; set; }

        [Required]
        [MaxLength(127)]
        public string Name { get; set; }
    }
}