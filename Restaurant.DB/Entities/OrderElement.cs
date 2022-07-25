using Restaurant.DB.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.DB.Entities
{
    public class OrderElement
    {
        public long Id { get; set; }
        public int MealId { get; set; }

        [Column(TypeName = "money")]
        [Required]
        public decimal CurrentPrice { get; set; }

        [Required]
        public short Amount { get; set; }

        [ForeignKey("Id")]
        public virtual Order Order { get; set; }

        [ForeignKey("MealId")]
        public virtual Meal Meal { get; set; }
    }
}
