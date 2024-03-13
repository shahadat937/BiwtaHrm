using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.appraisalFormType.Validators
{
    public class CreateappraisalFormTypeDtoValidator: AbstractValidator<CreateappraisalFormTypeDto>
    {
        public CreateappraisalFormTypeDtoValidator()
        {
            Include(new IappraisalFormTypeDtoValidator());
        }
    }
}
