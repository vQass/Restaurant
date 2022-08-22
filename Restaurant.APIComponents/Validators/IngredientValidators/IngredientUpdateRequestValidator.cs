using FluentValidation;
using Restaurant.Data.Models.IngredientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.APIComponents.Validators.IngredientValidators
{
    public class IngredientUpdateRequestValidator : AbstractValidator<IngredientUpdateRequest>
    {
        public IngredientUpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
