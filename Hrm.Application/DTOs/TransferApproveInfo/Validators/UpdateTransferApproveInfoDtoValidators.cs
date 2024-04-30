using FluentValidation;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.MaritalStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TransferApproveInfo.Validators
{
    public class UpdateTransferApproveInfoDtoValidators : AbstractValidator<TransferApproveInfoDto>
    {
        public UpdateTransferApproveInfoDtoValidators()
        {
            Include(new ITransferApproveInfoDtoValidator());

            RuleFor(x => x.TransferApproveInfoId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
