using Restaurant.DB.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.DB.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public short CityId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public long? PromotionCodeId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public DateTime OrderDate { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        [ForeignKey("PromotionCodeId")]
        public virtual Promotion Promotion { get; set; }
        public virtual List<OrderElement> OrderElements { get; set; }
    }
}
