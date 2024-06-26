using FluentValidation;
using Hrm.Application.DTOs.Shift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SiteVisit.Validators
{
    public class ISiteVisitDtoValidator: AbstractValidator<ISiteVisitDto>
    {
        public ISiteVisitDtoValidator() {
            RuleFor(data => data.EmpId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(data => data.VisitPlace).NotEmpty().WithMessage("{PropertyName} is required}").MaximumLength(450).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

        }
    }
}
