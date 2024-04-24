using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Institute;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Institute.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Institute.Handlers.Queries
{
    public class GetInstituteByIdRequestHandler : IRequestHandler<GetInstituteByIdRequest, InstituteDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Institute> _InstituteRepository;
        private readonly IMapper _mapper;
        public GetInstituteByIdRequestHandler(IHrmRepository<Hrm.Domain.Institute> InstituteRepositoy, IMapper mapper)
        {
            _InstituteRepository = InstituteRepositoy;
            _mapper = mapper;
        }

        public async Task<InstituteDto> Handle(GetInstituteByIdRequest request, CancellationToken cancellationToken)
        {
            var Institute = await _InstituteRepository.Get(request.InstituteId);
            return _mapper.Map<InstituteDto>(Institute);
        }
    }
}
