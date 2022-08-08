using FluentValidation;
using Restaurant.Data.Models.PromotionModels;
using Restaurant.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.APIComponents.Validators.PromotionValidators
{
    public class PromotionCreateRequestValidator : AbstractValidator<PromotionCreateRequest>
    { 
        public PromotionCreateRequestValidator()
        {
            var date = DateTime.Now;

            RuleFor(x => x.Code).NotEmpty().MaximumLength(255);

            RuleFor(x => x.DiscountPercentage).Custom((value, context) =>
            {
                if(value < 1 && value > 100)
                {
                    context.AddFailure("DiscountPercentage", "Wartość promocji musi znajdować się w przedziale od 1 od 100 procent");
                }
            });

            RuleFor(x => x.StartDate).NotNull().LessThan(x => x.EndDate);

            RuleFor(x => x.EndDate).NotNull().GreaterThan(date);
        }
    }
}
