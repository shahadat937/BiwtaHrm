using FluentValidation;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.MaritalStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.DepReleaseInfo.Validators
{
    public class UpdateDepReleaseInfoDtoValidators : AbstractValidator<DepReleaseInfoDto>
    {
        public UpdateDepReleaseInfoDtoValidators()
        {
            Include(new IDepReleaseInfoDtoValidator());

            RuleFor(x => x.DepReleaseInfoId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
