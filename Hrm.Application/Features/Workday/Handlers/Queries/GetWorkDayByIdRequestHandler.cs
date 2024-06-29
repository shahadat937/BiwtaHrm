using AutoMapper;
using Hrm.Application.Contracts.Persistence;
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
    public class GetWorkDayByIdRequestHandler:IRequestHandler<GetWorkdayByIdRequest,WorkdayDto>
    {
        IHrmRepository<Hrm.Domain.Workday> _Workdayrepository;
        IMapper _mapper;

        public GetWorkDayByIdRequestHandler(IHrmRepository<Domain.Workday> workdayrepository, IMapper mapper)
        {
            _Workdayrepository = workdayrepository;
            _mapper = mapper;
        }

        public async Task<WorkdayDto> Handle(GetWorkdayByIdRequest request, CancellationToken cancellationToken)
        {
            var Workday = await _Workdayrepository.Get(request.WorkdayId);

            return _mapper.Map<WorkdayDto>(Workday);
        }
    }
}
