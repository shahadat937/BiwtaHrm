using FluentValidation;
using Hrm.Application.DTOs.BloodGroup.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Union.Validators
{
    public class CreateUnionDtoValidator : AbstractValidator<CreateUnionDto>
    {
        public CreateUnionDtoValidator()
        {
            Include(new IUnionDtoValidators());
        }
    }
}
