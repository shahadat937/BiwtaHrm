using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Board.Validators
{
    public class IBoardDtoValidator : AbstractValidator<IBoardDto>
    {
        public IBoardDtoValidator()
        {
            RuleFor(b=>b.BoardName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
