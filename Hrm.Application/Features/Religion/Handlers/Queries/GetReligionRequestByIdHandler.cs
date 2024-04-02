using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.ChildStatus.Requests.Queries;
using Hrm.Application.Features.Religion.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Religion.Handlers.Queries
{
    public class ReligionRequest : IRequestHandler<GetReligionByIdRequest, ReligionDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Religion> _ReligionRepository;
        public ReligionRequest(IHrmRepository<Hrm.Domain.Religion> ReligionRepository, IMapper mapper)
        {
            _ReligionRepository = ReligionRepository;
            _mapper = mapper;
        }
        public async Task<ReligionDto> Handle(GetReligionByIdRequest request, CancellationToken cancellationToken)
        {
            var ChildStatus = await _ReligionRepository.Get(request.ReligionId);
            return _mapper.Map<ReligionDto>(ChildStatus);
        }
    }
}
