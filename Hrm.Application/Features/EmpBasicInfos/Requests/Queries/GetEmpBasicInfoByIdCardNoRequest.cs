using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Requests.Queries
{
    public class GetEmpBasicInfoByIdCardNoRequest : IRequest<object>
    {
        public string IdCardNo { get; set; }
    }
}
