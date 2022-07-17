using FluentValidation;
using Restaurant.Data.Models.UserModels;
using Restaurant.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.APIComponents.Validators
{
    public class UserCreateRequestDtoValidator : AbstractValidator<UserCreateRequestDto>
    {

        public UserCreateRequestDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Błędna wartość w polu potwierdzenia hasła");

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(x => x.Email == value);

                if (emailInUse)
                {
                    context.AddFailure("Emial", "Adres email jest zajęty");
                }
            });

            RuleFor(x => x.Address).MaximumLength(255);

            RuleFor(x => x.Name).MaximumLength(127);

            RuleFor(x => x.Surname).MaximumLength(127);

            RuleFor(x => x.Phone).MaximumLength(32); // TODO consider validating phone number with regex
        }
    }
}
