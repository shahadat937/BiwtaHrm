using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Weekend;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Weekend.Handlers.Queries
{
    public class GetWeekendRequestHandler : IRequestHandler<GetWeekendRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Weekend> _WeekendRepository;
        private readonly IMapper _mapper;
        public GetWeekendRequestHandler(IHrmRepository<Hrm.Domain.Weekend> WeekendRepository, IMapper mapper)
        {
            _WeekendRepository = WeekendRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetWeekendRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Weekend> Weekend = _WeekendRepository.Where(x => true);

            var WeekendDtos = _mapper.Map<List<WeekendDto>>(Weekend);

            return WeekendDtos;
        }
    }
}
