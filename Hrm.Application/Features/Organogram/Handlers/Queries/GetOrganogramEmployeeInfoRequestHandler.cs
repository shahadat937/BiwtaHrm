using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Organograms;
using Hrm.Application.Features.Organogram.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Organogram.Handlers.Queries
{
    //public class GetOrganogramEmployeeInfoRequestHandler : IRequestHandler<GetOrganogramEmployeeInfoRequest, List<OrganogramEmployeeInfo>>
    //{
    //    private readonly IHrmRepository<Hrm.Domain.EmpBasicInfo> _EmpBasicInfoRepository;
    //    private readonly IMapper _mapper;
    //    public GetOrganogramEmployeeInfoRequestHandler(IHrmRepository<Hrm.Domain.EmpBasicInfo> EmpBasicInfoRepository, IMapper mapper)
    //    {
    //        _EmpBasicInfoRepository = EmpBasicInfoRepository;
    //        _mapper = mapper; 
            
    //    }
    //    public async Task<List<OrganogramEmployeeInfo>> Handle(GetOrganogramEmployeeInfoRequest request, CancellationToken cancellationToken)
    //    {
    //        var empInfo = _EmpBasicInfoRepository.FilterWithInclude(x=> x.EmpJobDetail.)
    //    }
    //}
}
