using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Thana;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Thana.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Handlers.Queries
{
    public class GetThanaByIdRequestHandler : IRequestHandler<GetThanaByIdRequest, ThanaDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Thana> _ThanaRepository;
        private readonly IMapper _mapper;
        public GetThanaByIdRequestHandler(IHrmRepository<Hrm.Domain.Thana> ThanaRepositoy, IMapper mapper)
        {
            _ThanaRepository = ThanaRepositoy;
            _mapper = mapper;
        }

        public async Task<ThanaDto> Handle(GetThanaByIdRequest request, CancellationToken cancellationToken)
        {
            var Thana = await _ThanaRepository.Get(request.ThanaId);
            return _mapper.Map<ThanaDto>(Thana);
        }
    }
}
