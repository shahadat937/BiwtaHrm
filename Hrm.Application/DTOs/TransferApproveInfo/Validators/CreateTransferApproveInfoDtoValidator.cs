using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TransferApproveInfo.Validators
{
    public class CreateTransferApproveInfoDtoValidator : AbstractValidator<CreateTransferApproveInfoDto>
    {
        public CreateTransferApproveInfoDtoValidator()
        {
            Include(new ITransferApproveInfoDtoValidator());
        }
    }
}
