using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.OrderModels
{
    public class OrderAdminPanelWrapper
    {
        public List<OrderAdminPanelViewModel> Items { get; set; }
        public int ItemsCount { get; set; }
    }
}
