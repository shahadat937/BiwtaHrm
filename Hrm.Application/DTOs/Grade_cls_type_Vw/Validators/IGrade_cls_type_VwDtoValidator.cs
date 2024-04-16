using FluentValidation;
using Hrm.Application.DTOs.Grade_cls_type_Vw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Grade_cls_type_Vw.Validators
{
 
    public class IGrade_cls_type_VwDtoValidator : AbstractValidator<IGrade_cls_type_VwDto>
    {
        public IGrade_cls_type_VwDtoValidator()
        {
            RuleFor(b => b.GradeName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
