using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Board;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Board.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Board.Handlers.Queries
{
    public class GetBoardRequestHandler : IRequestHandler<GetBoardRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Board> _BoardRepository;
        private readonly IMapper _mapper;
        public GetBoardRequestHandler(IHrmRepository<Hrm.Domain.Board> BoardRepository, IMapper mapper)
        {
            _BoardRepository = BoardRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetBoardRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Board> Boards = _BoardRepository.Where(x => true);
            Boards = Boards.OrderByDescending(x => x.BoardId);

            var BoardDtos = _mapper.Map<List<BoardDto>>(Boards);

            return BoardDtos;
        }
    }
}
