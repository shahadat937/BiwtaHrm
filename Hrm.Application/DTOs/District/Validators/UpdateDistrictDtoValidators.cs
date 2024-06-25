using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.District.Validators
{
    public class UpdateDistrictDtoValidators:AbstractValidator<DistrictDto>
    {
        public UpdateDistrictDtoValidators()
        {
            Include(new IDistrictDtoValidator());
            RuleFor(x => x.DistrictId).NotNull().WithMessage("{propertyName } must be present");
        }
    }
}
