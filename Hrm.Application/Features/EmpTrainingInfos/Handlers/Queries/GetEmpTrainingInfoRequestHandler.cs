using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTrainingInfo;
using Hrm.Application.Features.EmpTrainingInfos.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTrainingInfos.Handlers.Queries
{
    public class GetEmpTrainingInfoRequestHandler: IRequestHandler<GetEmpTrainingInfoRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.EmpTrainingInfo> _empTrainingInfoRepo;
        private readonly IMapper _mapper;

        public GetEmpTrainingInfoRequestHandler(IHrmRepository<Domain.EmpTrainingInfo> empTrainingInfoRepo, IMapper mapper)
        {
            _empTrainingInfoRepo = empTrainingInfoRepo;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpTrainingInfoRequest request, CancellationToken cancellationToken)
        {
            var query = _empTrainingInfoRepo.Where(x => true)
                                .Include(x => x.Country)
                                .Include(x => x.TrainingType).AsQueryable();

            if(request.Filters.EmpId.HasValue)
            {
                query = query.Where(x=>x.EmpId == request.Filters.EmpId);
            }

            if(request.Filters.TrainingTypeId.HasValue)
            {
                query = query.Where(x => x.TrainingTypeId == request.Filters.TrainingTypeId);
            }

            if(request.Filters.StartDate.HasValue&&request.Filters.EndDate.HasValue)
            {
                query = query.Where(x => x.FromDate >= DateOnly.FromDateTime((DateTime)request.Filters.EndDate) && x.ToDate >= DateOnly.FromDateTime((DateTime)request.Filters.StartDate));
            }

            var empTrainingInfoDto = _mapper.Map<List<EmpTrainingInfoDto>>(await query.ToListAsync());

            return empTrainingInfoDto;
        }
    }
}
