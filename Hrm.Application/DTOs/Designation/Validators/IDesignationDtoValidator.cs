using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Designation.Validators
{

    
        public class IDesignationDtoValidator : AbstractValidator<IDesignationDto>
        {
            public IDesignationDtoValidator()
            {
                //RuleFor(b => b.DesignationName)
                //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
