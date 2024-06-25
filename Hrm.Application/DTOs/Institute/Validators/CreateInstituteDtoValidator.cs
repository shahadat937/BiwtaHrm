using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Institute.Validators
{
    public class CreateInstituteDtoValidator :AbstractValidator<CreateInstituteDto>
    {
        public CreateInstituteDtoValidator()
        {
            Include(new IInstituteDtoValidator());
        }
    }
}
