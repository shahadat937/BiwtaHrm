using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SelectableOption.Validators
{
    public class UpdateSelectableOptionDtoValidator: AbstractValidator<SelectableOptionDto>
    {
        public UpdateSelectableOptionDtoValidator()
        {
            RuleFor(e => e.OptionId).NotEmpty().WithMessage("{PropertyName} is required");
            Include(new ISelectableOptionDtoValidator());
        }
    }
}
