using FluentValidation;
using Hrm.Application.DTOs.ScaleGradeView.Validators;
using Hrm.Application.DTOs.ScaleGradeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ScaleGradeView.Validators
{

    public class CreateScaleGradeViewDtoValidator : AbstractValidator<CreateScaleGradeViewDto>
    {
        public CreateScaleGradeViewDtoValidator()
        {
            Include(new IScaleGradeViewDtoValidator());
        }
    }
}
