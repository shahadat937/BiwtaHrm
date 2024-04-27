using FluentValidation;
using Hrm.Application.DTOs.Board;
using Hrm.Application.DTOs.Board.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Board.Validators
{
    public class CreateBoardDtoValidator: AbstractValidator<CreateBoardDto>
    {
        public CreateBoardDtoValidator()
        {
            Include(new IBoardDtoValidator());
        }
    }
}
