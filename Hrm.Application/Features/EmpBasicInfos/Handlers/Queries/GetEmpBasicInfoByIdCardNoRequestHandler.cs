using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Queries
{
    public class GetEmpBasicInfoByIdCardNoRequestHandler : IRequestHandler<GetEmpBasicInfoByIdCardNoRequest, object>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpBasicInfoByIdCardNoRequestHandler(IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository, IMapper mapper)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpBasicInfoByIdCardNoRequest request, CancellationToken cancellationToken)
        {
            //var EmpBasicInfo = _EmpBasicInfoRepository.FinedOneInclude(x => x.IdCardNo == request.IdCardNo);

            var EmpBasicInfo = await _EmpBasicInfoRepository.Where(x => x.IdCardNo == request.IdCardNo)
                .Include(x => x.EmpJobDetail)
                    .ThenInclude(x => x.Department)
                .Include(x => x.EmpJobDetail)
                    .ThenInclude(x=>x.Designation)
                        .ThenInclude(x=>x.DesignationSetup)
                .Include(x=>x.EmpPhotoSign)
                .Include(x=>x.EmpJobDetail)
                    .ThenInclude(x=>x.Section)
                .FirstOrDefaultAsync();

            var EmpBasicInfoDto = _mapper.Map<EmpBasicInfoDto>(EmpBasicInfo);

            return EmpBasicInfoDto;
        }
    }
}
