using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Subject.Validators
{
    public class UpdateSubjectDtoValidator : AbstractValidator<SubjectDto>
    {
        public UpdateSubjectDtoValidator()
        {
            Include(new ISubjectDtoValidator());

            RuleFor(x => x.SubjectId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
