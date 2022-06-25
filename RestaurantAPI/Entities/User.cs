using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Entities
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(320)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public byte RoleId { get; set; }
        public UserDetails UserDetails { get; set; }
        public virtual Role Role { get; set; }
    }
}
