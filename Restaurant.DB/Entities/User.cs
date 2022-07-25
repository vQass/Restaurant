using Restaurant.DB.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.DB.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(320)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "tinyint")]
        public RoleEnum Role { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public virtual UserDetails UserDetails { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
