using FluentValidation;
using Hrm.Application.DTOs.GradeType.Validators;
using Hrm.Application.DTOs.GradeClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.GradeClass.Validators
{
    public class CreateGradeClassDtoValidator : AbstractValidator<CreateGradeClassDto>
    {
        public CreateGradeClassDtoValidator()
        {
            Include(new IGradeClassDtoValidator());
        }
    }
}
