using FluentValidation;
using Hrm.Application.DTOs.Language;
using Hrm.Application.DTOs.Language.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LanguageLanguage.Validators
{
    public class CreateLanguageDtoValidator: AbstractValidator<CreateLanguageDto>
    {
        public CreateLanguageDtoValidator()
        {
            Include(new ILanguageDtoValidator());
        }
    }
}
