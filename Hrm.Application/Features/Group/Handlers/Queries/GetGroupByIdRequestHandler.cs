using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Group;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Group.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Group.Handlers.Queries
{
    public class GetGroupByIdRequestHandler : IRequestHandler<GetGroupByIdRequest, GroupDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Group> _GroupRepository;
        private readonly IMapper _mapper;
        public GetGroupByIdRequestHandler(IHrmRepository<Hrm.Domain.Group> GroupRepositoy, IMapper mapper)
        {
            _GroupRepository = GroupRepositoy;
            _mapper = mapper;
        }

        public async Task<GroupDto> Handle(GetGroupByIdRequest request, CancellationToken cancellationToken)
        {
            var Group = await _GroupRepository.Get(request.GroupId);
            return _mapper.Map<GroupDto>(Group);
        }
    }
}
