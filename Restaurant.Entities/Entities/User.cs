using Restaurant.Entities.Enums;

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
        public virtual City City { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
