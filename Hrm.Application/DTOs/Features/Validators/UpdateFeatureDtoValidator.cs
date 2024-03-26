using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.Features.Validators
{
    public class UpdateFeatureDtoValidator : AbstractValidator<FeatureDto>
    {
        public UpdateFeatureDtoValidator()
        {
            Include(new IFeatureDtoValidator()); 

            RuleFor(p => p.FeatureId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
