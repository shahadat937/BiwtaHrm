using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.District.Validators
{
    public class CreateDistrictDtoValidator :AbstractValidator<CreateDistrictDto>
    {
        public CreateDistrictDtoValidator()
        {
            Include(new IDistrictDtoValidator());
        }
    }
}
