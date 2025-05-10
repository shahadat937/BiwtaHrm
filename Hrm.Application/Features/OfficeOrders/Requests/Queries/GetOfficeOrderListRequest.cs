using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.OfficeOrder;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeOrders.Requests.Queries
{
    public class GetOfficeOrderListRequest : IRequest<PagedResult<OfficeOrderDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? OrderTypeId { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? DesignationId { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? OrderNo { get; set; }
    }
}
