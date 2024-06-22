using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Hrm.Application.Features.EmpPhotoSigns.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPhotoSigns.Handlers.Queries
{
    public class GetEmpPhotoSignByIdRequestHandler : IRequestHandler<GetEmpPhotoSignByIdRequest, object>
    {

        private readonly IHrmRepository<EmpPhotoSign> _EmpPersonalInfoRepository;
        private readonly IMapper _mapper;
        public GetEmpPhotoSignByIdRequestHandler(IHrmRepository<EmpPhotoSign> EmpPersonalInfoRepository, IMapper mapper)
        {
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpPhotoSignByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpPhotoSign = _EmpPersonalInfoRepository.FinedOneInclude(x => x.EmpId == request.Id);

            return EmpPhotoSign;
        }
    }
}