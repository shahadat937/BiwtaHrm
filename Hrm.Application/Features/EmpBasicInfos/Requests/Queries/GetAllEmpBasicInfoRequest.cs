using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Requests.Queries
{
    public class GetAllEmpBasicInfoRequest : IRequest<PagedResult<EmpBasicInfoDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
