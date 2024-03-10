using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Thana_Upazila.Validators
{
    public class UpdateThana_UpazilaDtoValidator : AbstractValidator<Thana_UpazilaDto>
    {
        public UpdateThana_UpazilaDtoValidator()
        {
            Include(new IThana_UpazilaDtoValidators());

            RuleFor(x => x.Thana_UpazilaId).NotNull().WithMessage("{PropertyName} must be present");
        }   
    }
}
