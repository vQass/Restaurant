﻿using System.ComponentModel.DataAnnotations;

namespace Restaurant.DB.Entities
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(127)]
        public string Name { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}

