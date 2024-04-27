using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Board;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Board.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Board.Handlers.Queries
{
    public class GetExamTypeByIdRequestHandler : IRequestHandler<GetBoardByIdRequest, BoardDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Board> _BoardRepository;
        private readonly IMapper _mapper;
        public GetExamTypeByIdRequestHandler(IHrmRepository<Hrm.Domain.Board> BoardRepositoy, IMapper mapper)
        {
            _BoardRepository = BoardRepositoy;
            _mapper = mapper;
        }

        public async Task<BoardDto> Handle(GetBoardByIdRequest request, CancellationToken cancellationToken)
        {
            var Board = await _BoardRepository.Get(request.BoardId);
            return _mapper.Map<BoardDto>(Board);
        }
    }
}
