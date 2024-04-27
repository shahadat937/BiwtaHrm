using Hrm.Application.DTOs.Board;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Board.Requests.Queries
{
    public class GetBoardByIdRequest : IRequest<BoardDto>
    {
        public int BoardId { get; set; }
    }
}
