using Restaurant.Entities.Enums;

namespace Restaurant.Entities.Entities
{
    public class Order
    {
        public Order()
        {
            OrderElements = new List<OrderElement>();
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public short CityId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public long? PromotionCodeId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual User User { get; set; }
        public virtual City City { get; set; }
        public virtual List<OrderElement> OrderElements { get; set; }
    }
}
