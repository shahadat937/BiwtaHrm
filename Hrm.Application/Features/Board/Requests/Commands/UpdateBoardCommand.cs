using Hrm.Application.DTOs.Board;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Board.Requests.Commands
{
    public class UpdateExamTypeCommand : IRequest<BaseCommandResponse>
    {
        public BoardDto BoardDto { get; set; }
    }
}
