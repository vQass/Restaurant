using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.DB.Entities
{
    public class UserDetails
    {
        [Key]
        public long UserId { get; set; }

        [MaxLength(127)]
        public string? Name { get; set; }

        [MaxLength(127)]
        public string? Surname { get; set; }

        public short? CityId { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(32)]
        [Phone]
        public string? PhoneNumber { get; set; }

        public DateTime Inserted { get; set; }

        public DateTime Updated { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("CityId")]
        public virtual City? City { get; set; }
    }
}
