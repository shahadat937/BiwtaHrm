using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Group;
using Hrm.Application.DTOs.Group;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Group.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Group.Handlers.Queries
{
    public class GetGroupRequestHandler : IRequestHandler<GetGroupRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.SubGroup> _GroupRepository;
        private readonly IMapper _mapper;
        public GetGroupRequestHandler(IHrmRepository<Hrm.Domain.SubGroup> GroupRepository, IMapper mapper)
        {
            _GroupRepository = GroupRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetGroupRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.SubGroup> Group = _GroupRepository.Where(x => true);
            Group = Group.OrderByDescending(x => x.GroupId);

            var GroupDtos = await Task.Run(() => _mapper.Map<List<GroupDto>>(Group));

            return GroupDtos;
        }
    }
}