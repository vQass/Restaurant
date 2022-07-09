using Restaurant.API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.API.Entities
{
    public class OrderDetails
    {
        [Key]
        [Required]
        public long OrderId { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public short CityId { get; set; }

        [Required]
        [MaxLength(127)]
        public string Name { get; set; }

        [Required]
        [MaxLength(127)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(32)]
        [Phone]
        public string PhoneNumber { get; set; }

        public long? PromotionCodeId { get; set; }

        [Column(TypeName = "tinyint")]
        public OrderStatusEnum Status { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime OrderDate { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        [ForeignKey("PromotionCodeId")]
        public virtual Promotion Promotion { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
