using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.appraisalFormType.Validators
{
    public class UpdateappraisalFormTypeDtoValidator : AbstractValidator<appraisalFormTypeDto>
    {
        public UpdateappraisalFormTypeDtoValidator()
        { 
            Include(new IappraisalFormTypeDtoValidator());
            RuleFor(x=>x.appraisalFormTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
