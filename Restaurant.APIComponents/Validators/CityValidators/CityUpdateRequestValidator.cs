using FluentValidation;
using Restaurant.Data.Models.CityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.APIComponents.Validators.CityValidators
{
    public class CityUpdateRequestValidator : AbstractValidator<CityUpdateRequest>
    {
        public CityUpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(127);
        }
    }
}
