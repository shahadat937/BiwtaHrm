using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.ChildStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ChildStatus.Handlers.Queries
{
    public class GetChildStatusGetByIdHandler : IRequestHandler<GetChildStatusGetByIdRequest, ChildStatusDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.ChildStatus> _ChildStatusRepository;
        public GetChildStatusGetByIdHandler(IHrmRepository<Hrm.Domain.ChildStatus> ChildStatusRepository, IMapper mapper)
        {
            _ChildStatusRepository = ChildStatusRepository;
            _mapper = mapper;
        }
        public async Task<ChildStatusDto> Handle(GetChildStatusGetByIdRequest request, CancellationToken cancellationToken)
        {
            var ChildStatus = await _ChildStatusRepository.Get(request.ChildStatusId);
            return _mapper.Map<ChildStatusDto>(ChildStatus);
        }
    }
}
