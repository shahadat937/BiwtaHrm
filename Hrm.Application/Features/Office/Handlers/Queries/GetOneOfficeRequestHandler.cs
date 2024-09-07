using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Office;
using Hrm.Application.Features.Office.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Office.Handlers.Queries
{
    public class GetOneOfficeRequestHandler : IRequestHandler<GetOneOfficeRequest, OfficeDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Office> _OfficeRepository;
        private readonly IMapper _mapper;
        public GetOneOfficeRequestHandler(IHrmRepository<Hrm.Domain.Office> OfficeRepositoy, IMapper mapper)
        {
            _OfficeRepository = OfficeRepositoy;
            _mapper = mapper;
        }

        public async Task<OfficeDto> Handle(GetOneOfficeRequest request, CancellationToken cancellationToken)
        {
            var office = (await _OfficeRepository.GetAll())
                .OrderByDescending(o => o.OfficeId)
                .FirstOrDefault();
            return _mapper.Map<OfficeDto>(office);
        }
    }
}