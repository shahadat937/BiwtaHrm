using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ExamType.Validators
{

    
        public class IExamTypeDtoValidator : AbstractValidator<IExamTypeDto>
        {
            public IExamTypeDtoValidator()
            {
                RuleFor(b => b.ExamTypeName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
