﻿using System.ComponentModel.DataAnnotations;

namespace Restaurant.Data.Models.OrderModels
{
    public class OrderCreateRequest
    {
        public short UserId { get; set; }
        public long CityId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public virtual List<OrderElementCreateRequest> OrderElements { get; set; }
    }
}
