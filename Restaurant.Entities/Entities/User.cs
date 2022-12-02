using Restaurant.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Entities.Entities
{
    public class User
    {
        public User()
        {
            Orders = new List<Order>();
        }

        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public bool IsActive { get; set; } = true;
        public string Name { get; set; }
        public string Surname { get; set; }
        public short? CityId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }

        public virtual City City { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
