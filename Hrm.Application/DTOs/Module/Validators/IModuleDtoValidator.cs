using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.Modules.Validators
{
    public class IModuleDtoValidator : AbstractValidator<IModuleDto>
    {
        public string Title { get; set; }
        public string ModuleName { get; set; }
        public string IconName { get; set; }
        public string Icon { get; set; }
        public string Class { get; set; }
        public string GroupTitle { get; set; }

        public IModuleDtoValidator()
        {
            RuleFor(p => p.Title)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull()
           .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.ModuleName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.IconName)
                 .NotEmpty().WithMessage("{PropertyName} is required.")
                 .NotNull()
                 .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            //RuleFor(p => p.Icon)
            //  .NotEmpty().WithMessage("{PropertyName} is required.")
            //  .NotNull()
            //  .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            //RuleFor(p => p.Class)
            //  .NotEmpty().WithMessage("{PropertyName} is required.")
            //  .NotNull()
            //  .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.GroupTitle)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull()
              .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
