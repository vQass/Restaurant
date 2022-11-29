using FluentValidation;
using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.DB;

namespace Restaurant.APIComponents.Validators.UserValidators
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {

        public UserCreateRequestValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Błędna wartość w polu potwierdzenia hasła");

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(x => x.Email.ToLower() == value.ToLower());

                if (emailInUse)
                {
                    context.AddFailure("Emial", "Adres email jest zajęty");
                }
            });

            RuleFor(x => x.CityId).Custom((value, context) =>
            {
                if (value != null)
                {
                    var cityExists = dbContext.Cities.Any(x => x.Id == value);

                    if (!cityExists)
                    {
                        context.AddFailure("CityId", "Podane miasto nie występuje w bazie danych");
                    }
                }
            });

            RuleFor(x => x.Address).MaximumLength(255);

            RuleFor(x => x.Name).MaximumLength(127);

            RuleFor(x => x.Surname).MaximumLength(127);

            RuleFor(x => x.PhoneNumber).MaximumLength(32); // TODO consider validating phone number with regex
        }
    }
}
