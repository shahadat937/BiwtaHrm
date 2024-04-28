using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AspNetUsers.Validators
{
    public class CreateAspNetUserDtoValidator : AbstractValidator<CreateAspNetUserDto>
    {
        public CreateAspNetUserDtoValidator()
    {
        Include(new IAspNetUserDtoValidator());
    }
}
}
