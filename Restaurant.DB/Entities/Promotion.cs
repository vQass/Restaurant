using System.ComponentModel.DataAnnotations;

namespace Restaurant.DB.Entities
{
    public class Promotion
    {
        public long Id { get; set; }
        public string Code { get; set; }
        [Range(1, 100)]
        public byte DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsManuallyDisabled { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
