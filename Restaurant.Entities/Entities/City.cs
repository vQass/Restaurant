namespace Restaurant.Entities.Entities
{
    public class City
    {
        public City()
        {
            Orders = new List<Order>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<User> Users { get; set; }
    }
}