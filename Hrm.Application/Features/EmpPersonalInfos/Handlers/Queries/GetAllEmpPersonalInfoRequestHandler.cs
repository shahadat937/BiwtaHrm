using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpPersonalInfo;
using Hrm.Application.DTOs.Employee;
using Hrm.Application.Features.Employee.Requests.Queries;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpPersonalInfos.Handlers.Queries
{
    public class GetAllEmpPersonalInfoRequestHandler : IRequestHandler<GetAllEmpPersonalInfoRequest, object>
    {

        private readonly IHrmRepository<EmpPersonalInfo> _EmpPersonalInfoRepository;
        private readonly IMapper _mapper;
        public GetAllEmpPersonalInfoRequestHandler(IHrmRepository<EmpPersonalInfo> EmpPersonalInfoRepository, IMapper mapper)
        {
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAllEmpPersonalInfoRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpPersonalInfo> EmpPersonalInfo = _EmpPersonalInfoRepository.Where(x => true)
                .Include(x => x.Gender)
                .Include(x => x.MaritalStatus)
                .Include(x => x.BloodGroup)
                .Include(x => x.Religion)
                .Include(x => x.HairColor)
                .Include(x => x.EyesColor)
                .Include(x => x.Country);

            EmpPersonalInfo = EmpPersonalInfo.OrderByDescending(x => x.Id);

            var EmpPersonalInfoDtos = _mapper.Map<List<EmpPersonalInfoDto>>(EmpPersonalInfo);

            return EmpPersonalInfoDtos;
        }
    }
}