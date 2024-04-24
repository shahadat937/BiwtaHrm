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
    public class GetOfficeRequestHandler : IRequestHandler<GetOfficeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Office> _OfficeRepository;
        private readonly IMapper _mapper;
        public GetOfficeRequestHandler(IHrmRepository<Hrm.Domain.Office> OfficeRepository, IMapper mapper)
        {
            _OfficeRepository = OfficeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetOfficeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Office> Offices = _OfficeRepository.Where(x => true);

            var OfficeDtos = _mapper.Map<List<OfficeDto>>(Offices);

            return OfficeDtos;
        }
    }
}
