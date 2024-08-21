using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormRecord.Validators
{
    public class IFormRecordDtoValidator: AbstractValidator<IFormRecordDto>
    {
        public IFormRecordDtoValidator()
        {
            RuleFor(x => x.FormId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.EmpId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
