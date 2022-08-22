using FluentValidation;
using Restaurant.Data.Models.MealCategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.APIComponents.Validators.MealCategoryValidators
{
    public class MealCategoryUpdateRequestValidator : AbstractValidator<MealCategoryUpdateRequest>
    {
        public MealCategoryUpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
