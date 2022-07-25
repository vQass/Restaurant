using System.ComponentModel.DataAnnotations;

namespace Restaurant.DB.Entities
{
    public class Promotion
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Code { get; set; }

        [Required]
        [Range(1, 100)]
        public byte DiscountPercentage { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsManuallyDisabled { get; set; } = false;

        public virtual List<Order> Orders { get; set; }
    }
}
