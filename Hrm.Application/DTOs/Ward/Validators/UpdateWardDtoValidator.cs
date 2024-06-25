using FluentValidation;
using Hrm.Application.DTOs.Upazila.Validators;
using Hrm.Application.DTOs.Upazila;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Ward.Validators
{
    public class UpdateWardDtoValidator : AbstractValidator<WardDto>
    {
        public UpdateWardDtoValidator()
        {
            Include(new IWardDtoValidator());

            RuleFor(x => x.WardId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
