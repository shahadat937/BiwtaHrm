using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.Features.Validators
{
    public class IFeatureDtoValidator : AbstractValidator<IFeatureDto>
    {
        public string Path { get; set; }
        public string IconName { get; set; }
        public string Icon { get; set; }
        public string Class { get; set; }
        public string GroupTitle { get; set; }
        public int ModuleId { get; set; }
        public int FeatureCode { get; set; }
        public int OrderNo { get; set; }
        public bool IsReport { get; set; }
        public int FeatureTypeId { get; set; }
        public IFeatureDtoValidator()
        {
            RuleFor(p => p.FeatureName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(400).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.Path)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(500).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            //RuleFor(p => p.IconName)
            //  .NotEmpty().WithMessage("{PropertyName} is required.")
            //  .NotNull()
            //  .MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            //RuleFor(p => p.Icon)
            // .NotEmpty().WithMessage("{PropertyName} is required.")
            // .NotNull()
            // .MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

           // RuleFor(p => p.Class)
           //.NotEmpty().WithMessage("{PropertyName} is required.")
           //.NotNull()
           //.MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

          //RuleFor(p => p.GroupTitle)
          // .NotEmpty().WithMessage("{PropertyName} is required.")
          // .NotNull()
          // .MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

          //RuleFor(p => p.ModuleId)
          // .NotEmpty().WithMessage("{PropertyName} is required.");

          //RuleFor(p => p.GroupTitle)
          // .NotEmpty().WithMessage("{PropertyName} is required.")
          // .NotNull()
          // .MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

          //RuleFor(p => p.FeatureCode)
          // .NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}
