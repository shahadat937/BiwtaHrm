using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.PostingOrderInfo.Validators
{

    
        public class IPostingOrderInfoDtoValidator : AbstractValidator<IPostingOrderInfoDto>
        {
            public IPostingOrderInfoDtoValidator()
            {
                RuleFor(b => b.PostingOrderInfoName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
