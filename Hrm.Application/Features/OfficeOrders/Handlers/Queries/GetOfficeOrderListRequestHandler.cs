using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.DTOs.OfficeOrder;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
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
    public class GetOfficeOrderListRequestHandler : IRequestHandler<GetOfficeOrderListRequest, PagedResult<OfficeOrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<OfficeOrder> _OfficeOrderRepository;
        public GetOfficeOrderListRequestHandler(IMapper mapper, IHrmRepository<OfficeOrder> officeOrderRepository)
        {
            _mapper = mapper;
            _OfficeOrderRepository = officeOrderRepository;
        }

        public async Task<PagedResult<OfficeOrderDto>> Handle(GetOfficeOrderListRequest request, CancellationToken cancellationToken)
        {

            IQueryable<OfficeOrder> officeOrders = _OfficeOrderRepository.FilterWithInclude(x => 
                (request.OfficeId == 0 || x.OfficeId == request.OfficeId) &&
                (request.DepartmentId == 0 || x.DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.SectionId == request.SectionId) &&
                (request.DesignationId == 0 || x.DesignationId == request.DesignationId) &&
                (request.OrderTypeId == 0 || x.OrderTypeId == request.OrderTypeId) &&
                (request.OrderNo == null || x.OrderNo.ToLower().Contains(request.OrderNo.ToLower())) &&
                (request.FromDate == null || x.OrderDate >= request.FromDate) && 
                (request.ToDate == null || x.OrderDate <= request.ToDate))
                .Include(x => x.OrderType)
                .Include(x => x.Office)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.DesignationSetup)
                .OrderByDescending(x => x.OrderDate);

            var totalCount = await officeOrders.CountAsync(cancellationToken);

            var pagedResult = officeOrders
               .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
               .Take(request.QueryParams.PageSize)
               .ToList();

            var officeOrdersDto = _mapper.Map<List<OfficeOrderDto>>(pagedResult);

            var result = new PagedResult<OfficeOrderDto>(officeOrdersDto, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);


            return result;
        }
    }
}
