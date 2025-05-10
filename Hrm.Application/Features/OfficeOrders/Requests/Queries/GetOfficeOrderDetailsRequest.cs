using Hrm.Application.DTOs.OfficeOrder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeOrders.Requests.Queries
{
    public class GetOfficeOrderDetailsRequest : IRequest<OfficeOrderDto>
    {
        public int Id { get; set; }
    }
}
