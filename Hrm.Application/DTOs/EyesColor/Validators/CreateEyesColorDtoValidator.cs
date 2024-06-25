using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EyesColor.Validators
{
    public class CreateEyesColorDtoValidator : AbstractValidator<CreateEyesColorDto>
    {
        public CreateEyesColorDtoValidator()
        {
            Include(new IEyesColorDtoValidator());
        }
    }
}
