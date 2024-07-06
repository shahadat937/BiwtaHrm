using Hrm.Application.DTOs.EmpBasicInfo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Requests.Queries
{
    public class GetEmpBasicInfoByIdRequest : IRequest<object>
    {
        public int Id { get; set; }
    }
}