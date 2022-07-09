using System.ComponentModel.DataAnnotations;

namespace Restaurant.API.Entities
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