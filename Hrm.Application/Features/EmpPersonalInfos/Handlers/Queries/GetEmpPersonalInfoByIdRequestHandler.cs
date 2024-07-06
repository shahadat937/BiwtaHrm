using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpPersonalInfo;
using Hrm.Application.Features.Employee.Requests.Queries;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPersonalInfos.Handlers.Queries
{
    public class GetEmpPersonalInfoByIdRequestHandler : IRequestHandler<GetEmpPersonalInfoByIdRequest, object>
    {
        private readonly IHrmRepository<EmpPersonalInfo> _EmpPersonalInfoRepository;
        private readonly IMapper _mapper;

        public GetEmpPersonalInfoByIdRequestHandler(IHrmRepository<EmpPersonalInfo> EmpPersonalInfoRepository, IMapper mapper)
        {
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpPersonalInfoByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpPersonalInfo = await _EmpPersonalInfoRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.Gender)
                .Include(x => x.MaritalStatus)
                .Include(x => x.BloodGroup)
                .Include(x => x.Religion)
                .Include(x => x.HairColor)
                .Include(x => x.EyesColor)
                .Include(x => x.Country)
                .FirstOrDefaultAsync(cancellationToken);

            if (EmpPersonalInfo == null)
            {
                return null;
            }

            var EmpPersonalInfoDto = _mapper.Map<EmpPersonalInfoDto>(EmpPersonalInfo);

            return EmpPersonalInfoDto;
        }
    }
}