using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Office;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Office.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Office.Handlers.Queries
{
    public class GetOfficeByIdRequestHandler : IRequestHandler<GetOfficeByIdRequest, OfficeDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Office> _OfficeRepository;
        private readonly IMapper _mapper;
        public GetOfficeByIdRequestHandler(IHrmRepository<Hrm.Domain.Office> OfficeRepositoy, IMapper mapper)
        {
            _OfficeRepository = OfficeRepositoy;
            _mapper = mapper;
        }

        public async Task<OfficeDto> Handle(GetOfficeByIdRequest request, CancellationToken cancellationToken)
        {
            var Office = await _OfficeRepository.Get(request.OfficeId);
            return _mapper.Map<OfficeDto>(Office);
        }
    }
}
