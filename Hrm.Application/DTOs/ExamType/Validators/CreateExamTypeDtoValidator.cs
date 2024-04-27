using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ExamType.Validators
{
    public class CreateExamTypeDtoValidator : AbstractValidator<CreateExamTypeDto>
    {
        public CreateExamTypeDtoValidator()
        {
            Include(new IExamTypeDtoValidator());
        }
    }
}
