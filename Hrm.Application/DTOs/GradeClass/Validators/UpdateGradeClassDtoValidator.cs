using FluentValidation;
using Hrm.Application.DTOs.GradeType.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.GradeClass.Validators
{
    public class UpdateGradeClassDtoValidator : AbstractValidator<GradeClassDto>
    {
        public UpdateGradeClassDtoValidator()
        {
            Include(new IGradeClassDtoValidator());
            RuleFor(x => x.GradeClassId).NotNull().WithMessage("{propertyName } must be present");
        }
    }
}
