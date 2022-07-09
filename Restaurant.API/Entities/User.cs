using Restaurant.API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.API.Entities
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

        public virtual UserDetails UserDetails { get; set; }
    }
}
