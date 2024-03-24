using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Relation.Validators
{

    
        public class IRelationDtoValidator : AbstractValidator<IRelationDto>
        {
            public IRelationDtoValidator()
            {
                RuleFor(b => b.RelationName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
