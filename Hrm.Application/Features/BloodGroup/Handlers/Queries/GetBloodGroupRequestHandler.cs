using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.BloodGroup.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BloodGroup.Handlers.Queries
{
    public class GetBloodGroupRequestHandler : IRequestHandler<GetBloodGroupRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.BloodGroup> _bloodGroupRepository;
        private readonly IMapper _mapper;
        public GetBloodGroupRequestHandler(IHrmRepository<Hrm.Domain.BloodGroup> bloodGroupRepository, IMapper mapper)
        {
            _bloodGroupRepository = bloodGroupRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetBloodGroupRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.BloodGroup> bloodGroups = _bloodGroupRepository.Where(x => true);

            var BloodGroupDtos = _mapper.Map<List<BloodGroupDto>>(bloodGroups);

            return BloodGroupDtos;
        }
    }
}
