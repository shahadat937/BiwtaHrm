using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Division.Validators
{
    public class CreateDivisionDtoValidator : AbstractValidator<CreateDivisionDto>
    {
        public CreateDivisionDtoValidator()
        {
            Include(new IDivisionDtoValidator());
        }
    }
}
