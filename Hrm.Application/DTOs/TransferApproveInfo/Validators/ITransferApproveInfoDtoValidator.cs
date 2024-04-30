using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TransferApproveInfo.Validators
{


    public class ITransferApproveInfoDtoValidator : AbstractValidator<ITransferApproveInfoDto>
    {
        public ITransferApproveInfoDtoValidator()
        {
            RuleFor(b => b.TransferApproveInfoName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }

}
