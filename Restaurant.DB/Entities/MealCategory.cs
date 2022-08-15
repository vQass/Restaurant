﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Restaurant.DB.Entities
{
    public class MealCategory
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public virtual List<Meal> Meals { get; set; }
    }
}
