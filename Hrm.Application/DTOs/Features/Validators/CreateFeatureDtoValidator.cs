using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.Features.Validators
{
    public class CreateFeatureDtoValidator:AbstractValidator<CreateFeatureDto>
    {
        public CreateFeatureDtoValidator() 
        { 
            Include(new IFeatureDtoValidator());
        }
    }
}
