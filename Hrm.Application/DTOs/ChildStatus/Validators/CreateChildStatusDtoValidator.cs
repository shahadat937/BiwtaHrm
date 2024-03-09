using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ChildStatus.Validators
{
    public class CreateChildStatusDtoValidator : AbstractValidator<CreateChildStatusDto>
    {
        public CreateChildStatusDtoValidator()
        {
            Include(new IChildStatusDtoValidator());
        }
    }
}
