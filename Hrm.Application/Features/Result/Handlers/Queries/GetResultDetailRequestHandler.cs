using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Results.Requests.Queries;
using Hrm.Application.DTOs.Result;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Results.Handlers.Queries
{
    public class GetResultDetailRequestHandler : IRequestHandler<GetResultDetailRequest, ResultDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Result> _ResultRepository;
        public GetResultDetailRequestHandler(IHrmRepository<Hrm.Domain.Result> ResultRepository, IMapper mapper)
        {
            _ResultRepository = ResultRepository;
            _mapper = mapper;
        }
        public async Task<ResultDto> Handle(GetResultDetailRequest request, CancellationToken cancellationToken)
        {
            var Result = await _ResultRepository.Get(request.ResultId);
            return _mapper.Map<ResultDto>(Result);
        }
    }
}
