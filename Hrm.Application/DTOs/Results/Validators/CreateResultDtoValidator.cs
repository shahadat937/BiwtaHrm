using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Result.Validators
{
    public class CreateResultDtoValidator : AbstractValidator<CreateResultDto>
    {
        public CreateResultDtoValidator()
        {
            Include(new IResultDtoValidator());
        }
    }
}
