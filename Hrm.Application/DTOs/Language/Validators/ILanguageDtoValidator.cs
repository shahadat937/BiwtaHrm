using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Language.Validators
{
    public class ILanguageDtoValidator : AbstractValidator<ILanguageDto>
    {
        public ILanguageDtoValidator()
        {
            RuleFor(b=>b.LanguageName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
