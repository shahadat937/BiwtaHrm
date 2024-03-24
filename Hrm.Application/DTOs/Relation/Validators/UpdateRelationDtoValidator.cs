using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Relation.Validators
{
    public class UpdateRelationDtoValidator : AbstractValidator<RelationDto>
    {
        public UpdateRelationDtoValidator()
        {
            Include(new IRelationDtoValidator());

            RuleFor(x => x.RelationId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
