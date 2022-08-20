using FluentValidation;
using Restaurant.Data.Models.OrderModels;
using Restaurant.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            RuleFor(x => x.PromotionCode).Custom((code, context) =>
            {
                var codeTrimmed = code.Trim();
                var currentDateTime = DateTime.Now;

                if (!string.IsNullOrEmpty(codeTrimmed))
                {
                    var promotionCodeExists = dbContext.Promotions
                        .Any(x => x.Code == codeTrimmed);

                    if (!promotionCodeExists)
                    {
                        context.AddFailure("PromotionCode", "Promocja o podanym kodzie nie istnieje.");
                    }

                    var promotion = dbContext.Promotions
                        .FirstOrDefault(x => x.Code == codeTrimmed
                            && x.StartDate <= currentDateTime
                            && x.EndDate >= currentDateTime);

                    if (promotion is null)
                    {
                        context.AddFailure("PromotionCode", "Promocja o podanym kodzie jest nieaktywna.");
                    }

                    if (promotion.IsManuallyDisabled)
                    {
                        context.AddFailure("PromotionCode", "Promocja o podanym kodzie została wyłączona przez administratora.");

                    }
                }
            });
        }
    }
}
