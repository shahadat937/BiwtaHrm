using FluentValidation;
using Hrm.Application.DTOs.BloodGroup.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Upazila.Validators
{
    public class CreateUpazilaDtoValidator : AbstractValidator<CreateUpazilaDto>
    {
        public CreateUpazilaDtoValidator()
        {
            Include(new IUpazilaDtoValidators());
        }
    }
}
