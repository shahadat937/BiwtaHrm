using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SelectableOption.Validators
{
    public class CreateSelectableOptionDtoValidator: AbstractValidator<CreateSelectableOptionDto>
    {
        public CreateSelectableOptionDtoValidator()
        {
            Include(new ISelectableOptionDtoValidator());
        }
    }
}
