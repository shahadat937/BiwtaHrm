using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.DepReleaseInfo.Validators
{
    public class CreateDepReleaseInfoDtoValidator : AbstractValidator<CreateDepReleaseInfoDto>
    {
        public CreateDepReleaseInfoDtoValidator()
        {
            Include(new IDepReleaseInfoDtoValidator());
        }
    }
}
