using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SelectableOption.Validators
{
    public class ISelectableOptionDtoValidator : AbstractValidator<ISelectionOptionDto>
    {
        public ISelectableOptionDtoValidator()
        {
            RuleFor(e => e.FieldId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.OptionName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.OptionValue).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.IsActive).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
