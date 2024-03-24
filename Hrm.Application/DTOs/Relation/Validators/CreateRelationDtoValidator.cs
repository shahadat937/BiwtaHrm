using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Relation.Validators
{
    public class CreateRelationDtoValidator : AbstractValidator<CreateRelationDto>
    {
        public CreateRelationDtoValidator()
        {
            Include(new IRelationDtoValidator());
        }
    }
}
