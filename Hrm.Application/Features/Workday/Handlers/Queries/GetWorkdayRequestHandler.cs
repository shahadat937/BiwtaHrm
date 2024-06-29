using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SiteVisit;
using Hrm.Application.DTOs.Workday;
using Hrm.Application.Features.Workday.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Workday.Handlers.Queries
{
    public class GetWorkdayRequestHandler:IRequestHandler<GetWorkdayRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.Workday> _WorkdayRepository;
        IMapper _mapper;

        public GetWorkdayRequestHandler(IHrmRepository<Hrm.Domain.Workday> unitOfWork, IMapper mapper)
        {
            _WorkdayRepository = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetWorkdayRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Workday> Workdays = _WorkdayRepository.Where(x => true);

            Workdays = Workdays.OrderByDescending(x => x.WorkdayId);

            var WorkdayDto = _mapper.Map<List<WorkdayDto>>(Workdays);

            return WorkdayDto;
        }
    }
}
