using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Designation.Validators
{
    public class UpdateDesignationDtoValidator : AbstractValidator<DesignationDto>
    {
        public UpdateDesignationDtoValidator()
        {
            Include(new IDesignationDtoValidator());

            RuleFor(x => x.DesignationId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
