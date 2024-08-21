using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FieldRecord.Validators
{
    public class CreateFieldRecordDtoValidator: AbstractValidator<CreateFieldRecordDto>
    {
        public CreateFieldRecordDtoValidator()
        {
            Include(new IFieldRecordDtoValidator());
        }
    }
}
