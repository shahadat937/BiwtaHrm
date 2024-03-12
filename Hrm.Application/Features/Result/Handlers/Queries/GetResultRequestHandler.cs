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
            IQueryable<Hrm.Domain.Result> Results = _ResultRepository.Where(x => true);

            var ResultDtos = _mapper.Map<List<ResultDto>>(Results);

            return ResultDtos;
        }
    }
}
