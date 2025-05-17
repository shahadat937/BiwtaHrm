using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.OfficeOrder;
using Hrm.Application.Features.OfficeOrders.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeOrders.Handlers.Queries
{
    public class GetOfficeOrderDetailsRequestHandler : IRequestHandler<GetOfficeOrderDetailsRequest, OfficeOrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<OfficeOrder> _OfficeOrderRepository;
        public GetOfficeOrderDetailsRequestHandler(IMapper mapper, IHrmRepository<OfficeOrder> officeOrderRepository)
        {
            _mapper = mapper;
            _OfficeOrderRepository = officeOrderRepository;
        }

        public async Task<OfficeOrderDto> Handle(GetOfficeOrderDetailsRequest request, CancellationToken cancellationToken)
        {

            var officeOrderDetail = await _OfficeOrderRepository.Get(request.Id);

            var officeOrdersDto = _mapper.Map<OfficeOrderDto>(officeOrderDetail);

            return officeOrdersDto;
        }
    }
}
