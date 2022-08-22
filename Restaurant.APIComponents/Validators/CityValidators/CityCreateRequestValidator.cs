using FluentValidation;
using Restaurant.Data.Models.CityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.APIComponents.Validators.CityValidators
{
    public class CityCreateRequestValidator : AbstractValidator<CityCreateRequest>
    {
        public CityCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
