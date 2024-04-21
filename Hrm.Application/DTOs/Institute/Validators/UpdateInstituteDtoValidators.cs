using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Institute.Validators
{
    public class UpdateInstituteDtoValidators:AbstractValidator<InstituteDto>
    {
        public UpdateInstituteDtoValidators()
        {
            Include(new IInstituteDtoValidator());
            RuleFor(x => x.InstituteId).NotNull().WithMessage("{propertyName } must be present");
        }
    }
}
