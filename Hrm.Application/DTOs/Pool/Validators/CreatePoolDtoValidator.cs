using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Pool.Validators
{
    public class CreatePoolDtoValidator : AbstractValidator<CreatePoolDto>
    {
        public CreatePoolDtoValidator()
        {
            Include(new IPoolDtoValidator());
        }
    }
}
