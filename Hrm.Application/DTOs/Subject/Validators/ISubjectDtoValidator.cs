using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Subject.Validators
{

    
        public class ISubjectDtoValidator : AbstractValidator<ISubjectDto>
        {
            public ISubjectDtoValidator()
            {
                RuleFor(b => b.SubjectName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
