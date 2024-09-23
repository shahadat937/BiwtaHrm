using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpWorkHistories.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpWorkHistory;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpWorkHistories.Handlers.Queries
{
    public class GetEmpWorkHistoryByEmpIdRequestHandler : IRequestHandler<GetEmpWorkHistoryByEmpIdRequest, List<EmpWorkHistoryDto>>
    {

        private readonly IHrmRepository<EmpWorkHistory> _EmpWorkHistoryRepository;
        private readonly IMapper _mapper;
        public GetEmpWorkHistoryByEmpIdRequestHandler(IHrmRepository<EmpWorkHistory> EmpWorkHistoryRepository, IMapper mapper)
        {
            _EmpWorkHistoryRepository = EmpWorkHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpWorkHistoryDto>> Handle(GetEmpWorkHistoryByEmpIdRequest request, CancellationToken cancellationToken)
        {
            List<EmpWorkHistory> EmpWorkHistories = await _EmpWorkHistoryRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.Office)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.Designation)
                .ToListAsync(cancellationToken);

            if (EmpWorkHistories == null)
            {
                return null;
            }

            List<EmpWorkHistoryDto> result = _mapper.Map<List<EmpWorkHistoryDto>>(EmpWorkHistories);

            return result;
        }
    }
}