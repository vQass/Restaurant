﻿namespace Restaurant.Entities.Entities
{
    public class MealCategory
    {
        public MealCategory()
        {
            Meals = new List<Meal>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual List<Meal> Meals { get; set; }
    }
}
