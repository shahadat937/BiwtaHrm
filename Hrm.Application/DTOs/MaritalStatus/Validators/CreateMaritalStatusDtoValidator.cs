using FluentValidation;
using Hrm.Application.DTOs.BloodGroup.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.MaritalStatus.Validators
{
    public class CreateMaritalStatusDtoValidator : AbstractValidator<CreateMaritalStatusDto>
    {
        public CreateMaritalStatusDtoValidator()
        {
            Include(new IMaritalStatusDtoValidators());
        }
    }
}
