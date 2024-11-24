using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Hrm.Application.Features.EmpFingerPrints.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpFingerPrints.Handlers.Queries
{
    public class GetEmpFingerPrintByIdRequestHandler : IRequestHandler<GetEmpFingerPrintByIdRequest, object>
    {

        private readonly IHrmRepository<EmpFingerPrint> _EmpPersonalInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpFingerPrintByIdRequestHandler(IHrmRepository<EmpFingerPrint> EmpPersonalInfoRepository, IMapper mapper)
        {
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpFingerPrintByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpFingerPrint = _EmpPersonalInfoRepository.FinedOneInclude(x => x.EmpId == request.Id);

            return EmpFingerPrint;
        }
    }
}