﻿using FluentValidation;
using Restaurant.Data.Models.UserModels;
using Restaurant.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.APIComponents.Validators.UserValidators
{
    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateRequestValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.CityId).Custom((value, context) =>
            {
                var cityExists = dbContext.Cities.Any(x => x.Id == value);

                if (!cityExists)
                {
                    context.AddFailure("CityId", "Podane miasto nie występuje w bazie danych");
                }
            });

            RuleFor(x => x.Address).MaximumLength(255);

            RuleFor(x => x.Name).MaximumLength(127);

            RuleFor(x => x.Surname).MaximumLength(127);

            RuleFor(x => x.PhoneNumber).MaximumLength(32); // TODO consider validating phone number with regex
        }
    }
}
