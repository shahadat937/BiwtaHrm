using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Board.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Board.Handlers.Queries
{ 
    public class GetSelectedBoardRequestHandler : IRequestHandler<GetSelectedBoardRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Board> _BoardRepository;


        public GetSelectedBoardRequestHandler(IHrmRepository<Hrm.Domain.Board> BoardRepository)
        {
            _BoardRepository = BoardRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBoardRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Board> Boards = await _BoardRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Boards.Select(x => new SelectedModel 
            {
                Name = x.BoardName,
                Id = x.BoardId
            }).ToList();
            return selectModels;
        }
    }
}
 