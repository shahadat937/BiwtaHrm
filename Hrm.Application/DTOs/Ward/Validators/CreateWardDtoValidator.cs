using FluentValidation;
using Hrm.Application.DTOs.Religion.Validators;
using Hrm.Application.DTOs.Religion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Ward.Validators
{
    public class CreateWardDtoValidator : AbstractValidator<CreateWardDto>
    {
        public CreateWardDtoValidator()
        {
            Include(new IWardDtoValidator());
        }
    }
}