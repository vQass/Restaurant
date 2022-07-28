﻿using Restaurant.DB.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.DB.Entities
{
    public class OrderElement
    {
        public long OrderId { get; set; }
        public int MealId { get; set; }
        public decimal CurrentPrice { get; set; }
        public short Amount { get; set; }
        public virtual Order Order { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
