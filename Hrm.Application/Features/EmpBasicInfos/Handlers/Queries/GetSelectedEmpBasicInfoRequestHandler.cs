using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Queries
{
    public class GetSelectedEmpBasicInfoRequestHandler: IRequestHandler<GetSelectedEmpBasicInfoRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.EmpBasicInfo> _EmpBasicInfoRepository;
        
        public GetSelectedEmpBasicInfoRequestHandler(IHrmRepository<Hrm.Domain.EmpBasicInfo> EmpBasicInfoRepository)
        {
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<object> Handle(GetSelectedEmpBasicInfoRequest request, CancellationToken cancellationToken)
        {
            List<SelectedModel> EmpInfos = _EmpBasicInfoRepository.Where(x => true).Select(x => new SelectedModel
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName
            }).ToList();

            return EmpInfos;
        }
    }
}
