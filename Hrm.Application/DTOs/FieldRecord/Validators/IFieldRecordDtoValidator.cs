using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FieldRecord.Validators
{
    public class IFieldRecordDtoValidator: AbstractValidator<IFieldRecordDto>
    {
        public IFieldRecordDtoValidator()
        {
            RuleFor(x => x.FieldId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.FormRecordId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.FieldValue).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
