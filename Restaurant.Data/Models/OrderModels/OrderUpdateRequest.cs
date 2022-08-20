using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.OrderModels
{
    public class OrderUpdateRequest
    {
        public short CityId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PromotionCode { get; set; }
        public virtual List<OrderElementUpdateRequest> OrderElements { get; set; }
    }
}
