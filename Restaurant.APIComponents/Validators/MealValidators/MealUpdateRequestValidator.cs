﻿using FluentValidation;
using Restaurant.Data.Models.MealModels;
using Restaurant.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.APIComponents.Validators.MealValidators
{
    public class MealUpdateRequestValidator : AbstractValidator<MealUpdateRequest>
    {
        public MealUpdateRequestValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Price).GreaterThan(0).NotNull();

            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);

            RuleFor(x => x.CategoryId).Custom((value, context) =>
            {
                var validCategoryId = dbContext.MealsCategories.Any(x =>
                    x.Id == value);

                if (!validCategoryId)
                {
                    context.AddFailure("CategoryId", "Podana kategoria dania nie istnieje");
                }
            });
        }
    }
}