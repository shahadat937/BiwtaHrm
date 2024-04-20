using FluentValidation;
using Hrm.Application.DTOs.Language;
using Hrm.Application.DTOs.Language.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Language.ValidatorsLanguage
{
    public class UpdateLanguageDtoValidator : AbstractValidator<LanguageDto>
    {
        public UpdateLanguageDtoValidator()
        { 
            Include(new ILanguageDtoValidator());
            RuleFor(x=>x.LanguageId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
