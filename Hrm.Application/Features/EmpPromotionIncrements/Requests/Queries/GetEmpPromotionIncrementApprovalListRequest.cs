using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.EmpPromotionIncrement;
using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPromotionIncrements.Requests.Queries
{
    public class GetEmpPromotionIncrementApprovalListRequest : IRequest<PagedResult<EmpPromotionIncrementDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? Id { get; set; }
        //public int? EmpId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
