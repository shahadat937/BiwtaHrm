using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FieldRecord.Validators
{
    public class UpdateFieldRecordDtoValidator: AbstractValidator<FieldRecordDto>
    {
        public UpdateFieldRecordDtoValidator()
        {
            RuleFor(x => x.FieldRecordId).NotEmpty().WithMessage("{PropertyName} is required");
            Include(new IFieldRecordDtoValidator());
        }
    }
}
