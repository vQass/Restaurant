﻿namespace Restaurant.Data.Models.OrderModels
{
    public class OrderAdminPanelViewModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderElementViewModel> OrderElements { get; set; }
    }
}
