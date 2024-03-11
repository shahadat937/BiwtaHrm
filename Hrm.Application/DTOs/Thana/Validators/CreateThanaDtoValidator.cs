using FluentValidation;
using Hrm.Application.DTOs.BloodGroup.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Thana.Validators
{
    public class CreateThanaDtoValidator : AbstractValidator<CreateThanaDto>
    {
        public CreateThanaDtoValidator()
        {
            Include(new IThanaDtoValidators());
        }
    }
}
