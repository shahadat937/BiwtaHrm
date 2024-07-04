using AutoMapper;
using Hrm.Application.Contracts.Persistence;
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
using Hrm.Application.DTOs.EmpBasicInfo;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Queries
{
    public class GetEmpBasicInfoByIdRequestHandler : IRequestHandler<GetEmpBasicInfoByIdRequest, object>
    {
        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IMapper _mapper;

        public GetEmpBasicInfoByIdRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository, IMapper mapper)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpBasicInfoByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpBasicInfo = await _EmpBasicInfoRepository.Where(x => x.Id == request.Id)
                                                            .Include(x => x.EmployeeType)
                                                            .FirstOrDefaultAsync(cancellationToken);

            if (EmpBasicInfo == null)
            {
                return null; 
            }

            var EmpBasicInfoDto = _mapper.Map<EmpBasicInfoDto>(EmpBasicInfo);

            return EmpBasicInfoDto;
        }
    }

}