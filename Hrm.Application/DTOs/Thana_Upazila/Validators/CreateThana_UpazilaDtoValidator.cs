using FluentValidation;
using Hrm.Application.DTOs.BloodGroup.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Thana_Upazila.Validators
{
    public class CreateThana_UpazilaDtoValidator : AbstractValidator<CreateThana_UpazilaDto>
    {
        public CreateThana_UpazilaDtoValidator()
        {
            Include(new IThana_UpazilaDtoValidators());
        }
    }
}
