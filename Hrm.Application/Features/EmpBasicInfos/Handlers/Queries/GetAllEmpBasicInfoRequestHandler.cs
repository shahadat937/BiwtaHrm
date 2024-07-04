using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.DTOs.Employee;
using Hrm.Application.Features.Employee.Requests.Queries;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Queries
{
    public class GetAllEmpBasicInfoRequestHandler : IRequestHandler<GetAllEmpBasicInfoRequest, object>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IMapper _mapper;
        public GetAllEmpBasicInfoRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository, IMapper mapper)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAllEmpBasicInfoRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpBasicInfo> EmpBasicInfo = _EmpBasicInfoRepository.Where(x => true)
                .Include(x => x.EmployeeType); ;

            EmpBasicInfo = EmpBasicInfo.OrderByDescending(x => x.Id);

            var EmpBasicInfoDtos = _mapper.Map<List<EmpBasicInfoDto>>(EmpBasicInfo);

            return EmpBasicInfoDtos;
        }
    }
}