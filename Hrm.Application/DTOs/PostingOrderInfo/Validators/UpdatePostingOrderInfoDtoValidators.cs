using FluentValidation;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.MaritalStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.PostingOrderInfo.Validators
{
    public class UpdatePostingOrderInfoDtoValidators : AbstractValidator<PostingOrderInfoDto>
    {
        public UpdatePostingOrderInfoDtoValidators()
        {
            Include(new IPostingOrderInfoDtoValidator());

            RuleFor(x => x.PostingOrderInfoId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
