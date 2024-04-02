using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Result;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Result.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.Result;
using Hrm.Application.Features.Result.Requests.Queries;

namespace Hrm.Application.Features.Result.Handlers.Queries
{
    public class GetResultRequestHandler : IRequestHandler<GetResultRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Result> _ResultRepository;
        private readonly IMapper _mapper;
        public GetResultRequestHandler(IHrmRepository<Hrm.Domain.Result> ResultRepository, IMapper mapper)
        {
            _ResultRepository = ResultRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetResultRequest request, CancellationToken cancellationToken)
        {
            // Fetch blood groups from repository
            IQueryable<Hrm.Domain.Result> Results = _ResultRepository.Where(x => true);

            // Order blood groups by descending order
            Results = Results.OrderByDescending(x => x.ResultId);

            // Map the ordered blood groups to ResultDto
            var ResultDtos = _mapper.Map<List<ResultDto>>(Results.ToList());

            return ResultDtos;
        }
    }
}
