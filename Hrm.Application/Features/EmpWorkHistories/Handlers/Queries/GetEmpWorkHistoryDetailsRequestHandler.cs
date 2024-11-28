using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpWorkHistory;
using Hrm.Application.Features.EmpWorkHistories.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpWorkHistories.Handlers.Queries
{
    public class GetEmpWorkHistoryDetailsRequestHandler : IRequestHandler<GetEmpWorkHistoryDetailsRequest, EmpWorkHistoryDto>
    {

        private readonly IHrmRepository<EmpWorkHistory> _EmpWorkHistoryRepository;
        private readonly IMapper _mapper;
        public GetEmpWorkHistoryDetailsRequestHandler(IHrmRepository<EmpWorkHistory> EmpWorkHistoryRepository, IMapper mapper)
        {
            _EmpWorkHistoryRepository = EmpWorkHistoryRepository;
            _mapper = mapper;
        }

        public async Task<EmpWorkHistoryDto> Handle(GetEmpWorkHistoryDetailsRequest request, CancellationToken cancellationToken)
        {
            var EmpWorkHistories = _EmpWorkHistoryRepository.Where(x => x.Id == request.Id)
                .Include(x => x.Office)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.DesignationSetup)
                .Include(x => x.Designation)
                        .ThenInclude(ds => ds.DesignationSetup)
                .FirstOrDefault();


            var result = _mapper.Map<EmpWorkHistoryDto>(EmpWorkHistories);

            return result;
        }
    }
}