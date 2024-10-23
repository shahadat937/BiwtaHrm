using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ReleaseType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.ReleaseTypes.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ReleaseTypes.Handlers.Queries
{
    public class GetReleaseTypeByIdRequestHandler : IRequestHandler<GetReleaseTypeByIdRequest, ReleaseTypeDto>
    {

        private readonly IHrmRepository<Hrm.Domain.ReleaseType> _ReleaseTypeRepository;
        private readonly IMapper _mapper;
        public GetReleaseTypeByIdRequestHandler(IHrmRepository<Hrm.Domain.ReleaseType> ReleaseTypeRepositoy, IMapper mapper)
        {
            _ReleaseTypeRepository = ReleaseTypeRepositoy;
            _mapper = mapper;
        }

        public async Task<ReleaseTypeDto> Handle(GetReleaseTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var ReleaseType = await _ReleaseTypeRepository.Get(request.ReleaseTypeId);
            return _mapper.Map<ReleaseTypeDto>(ReleaseType);
        }
    }
}
