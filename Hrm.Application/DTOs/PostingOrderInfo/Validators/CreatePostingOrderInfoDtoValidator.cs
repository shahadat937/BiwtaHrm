using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.PostingOrderInfo.Validators
{
    public class CreatePostingOrderInfoDtoValidator : AbstractValidator<CreatePostingOrderInfoDto>
    {
        public CreatePostingOrderInfoDtoValidator()
        {
            Include(new IPostingOrderInfoDtoValidator());
        }
    }
}
