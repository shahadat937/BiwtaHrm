using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Queries
{
    public class GetEmpByFilterRequestHandler: IRequestHandler<GetEmpByFilterRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IMapper _mapper;
        
        public GetEmpByFilterRequestHandler(IHrmRepository<Hrm.Domain.EmpBasicInfo> EmpBasicInfoRepository, IMapper mapper)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpByFilterRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.EmpBasicInfo> EmpBasicInfo = _EmpBasicInfoRepository.Where(x => true)
                .Include(emp => emp.EmpJobDetail).AsQueryable();

            if(request.EmpFilterDto.OfficeId.HasValue)
            {
                EmpBasicInfo = EmpBasicInfo.Where(emp => emp.EmpJobDetail.Any() && emp.EmpJobDetail.ToList()[0].OfficeId == request.EmpFilterDto.OfficeId);
            }

            if(request.EmpFilterDto.DepartmentId.HasValue)
            {
                EmpBasicInfo = EmpBasicInfo.Where(emp =>emp.EmpJobDetail.Any() && emp.EmpJobDetail.ToList()[0].DepartmentId == request.EmpFilterDto.DepartmentId);
            }

            var EmpDto = _mapper.Map<List<Hrm.Domain.EmpBasicInfo>>(EmpBasicInfo);

            return EmpDto;
            
        }
    }
}
