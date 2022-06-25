using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Entities
{
    public class UserDetails
    {
        public string Name { get; set; }
        public short CityId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public City City { get; set; }
    }
}
