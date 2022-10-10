using System.ComponentModel.DataAnnotations;

namespace Restaurant.Data.Models.OrderModels
{
    public  class OrderCreateRequest
    {
        public short UserId { get; set; }
        public long CityId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        [Phone(ErrorMessage = "Błędny format numeru telefonu!")]
        public string PhoneNumber { get; set; }
        public string? PromotionCode { get; set; }
        public long? PromotionId { get; set; }
        public virtual List<OrderElementCreateRequest> OrderElements { get; set; }
    }
}
