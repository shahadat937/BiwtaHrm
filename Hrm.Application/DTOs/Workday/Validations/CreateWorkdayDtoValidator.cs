using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Workday.Validations
{
    public class CreateWorkdayDtoValidator : AbstractValidator<CreateWorkdayDto>
    {
        public CreateWorkdayDtoValidator() {
            Include(new IWorkdayDtoValidator());
        }
    }
}
