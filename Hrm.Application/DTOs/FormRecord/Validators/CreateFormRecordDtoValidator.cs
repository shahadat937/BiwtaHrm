using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormRecord.Validators
{
    public class CreateFormRecordDtoValidator: AbstractValidator<CreateFormRecordDto>
    {
        public CreateFormRecordDtoValidator()
        {
            Include(new IFormRecordDtoValidator());
        }
    }
}
