using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Upazila.Validators
{
    public class UpdateUpazilaDtoValidator : AbstractValidator<UpazilaDto>
    {
        public UpdateUpazilaDtoValidator()
        {
            Include(new IUpazilaDtoValidators());

            RuleFor(x => x.UpazilaId).NotNull().WithMessage("{PropertyName} must be present");
        }   
    }
}
