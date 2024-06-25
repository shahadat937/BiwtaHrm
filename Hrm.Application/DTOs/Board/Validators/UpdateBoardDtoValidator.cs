using FluentValidation;
using Hrm.Application.DTOs.Board;
using Hrm.Application.DTOs.Board.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Board.ValidatorsBoard
{
    public class UpdateBoardDtoValidator : AbstractValidator<BoardDto>
    {
        public UpdateBoardDtoValidator()
        { 
            Include(new IBoardDtoValidator());
            RuleFor(x=>x.BoardId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
