using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Requests.Queries
{
    public class GetEmployeeInfoRequest: IRequest<object>
    {
        public string? IdCardNo { get; set; }
    }
}
