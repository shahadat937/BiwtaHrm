using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Ward;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Ward.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Ward.Handlers.Queries
{
    public class GetWardByIdRequestHandler : IRequestHandler<GetWardByIdRequest, WardDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Ward> _WardRepository;
        private readonly IMapper _mapper;
        public GetWardByIdRequestHandler(IHrmRepository<Hrm.Domain.Ward> WardRepositoy, IMapper mapper)
        {
            _WardRepository = WardRepositoy;
            _mapper = mapper;
        }

        public async Task<WardDto> Handle(GetWardByIdRequest request, CancellationToken cancellationToken)
        {
            var Ward = await _WardRepository.Get(request.WardId);
            return _mapper.Map<WardDto>(Ward);
        }
    }
}
