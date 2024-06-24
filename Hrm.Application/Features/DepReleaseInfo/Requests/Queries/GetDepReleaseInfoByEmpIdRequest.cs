using Hrm.Application.DTOs.DepReleaseInfo;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DepReleaseInfo.Requests.Queries
{
    public class GetDepReleaseInfoByEmployeeIdRequest:IRequest<List<SelectedModel>>
    {
        public int EmpId { get; set; }
    }
}
