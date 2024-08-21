using FluentValidation;
using Hrm.Application.DTOs.FormField.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormRecord.Validators
{
    public class UpdateFormRecordDtoValidator: AbstractValidator<FormRecordDto>
    {
        public UpdateFormRecordDtoValidator()
        {
            RuleFor(x => x.RecordId).NotEmpty().WithMessage("{PropertyName} is required");
            Include(new IFormRecordDtoValidator());
        }
    }
}
