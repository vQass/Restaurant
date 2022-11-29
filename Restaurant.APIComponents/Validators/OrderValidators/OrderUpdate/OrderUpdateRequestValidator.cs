using FluentValidation;
using Restaurant.Data.Models.OrderModels;
using Restaurant.DB;

namespace Restaurant.APIComponents.Validators.OrderValidators.OrderUpdate
{
    public class OrderUpdateRequestValidator : AbstractValidator<OrderUpdateRequest>
    {
        public OrderUpdateRequestValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Address).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
            RuleFor(x => x.Surname).NotEmpty().MaximumLength(127);
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(32);

            RuleFor(x => x.CityId).Custom((cityId, context) =>
            {
                var cityExists = dbContext.Cities.Any(x => x.Id == cityId);

                if (!cityExists)
                {
                    context.AddFailure("CityId", "Miasto o podanym id nie istnieje.");
                }

                var cityInactive = dbContext.Cities.Where(x => !x.IsActive).Any(x => x.Id == cityId);

                if (cityInactive)
                {
                    context.AddFailure("CityId", "Miasto o podanym id zostało oznaczone jako nieaktywne.");

                }
            });
        }
    }
}
