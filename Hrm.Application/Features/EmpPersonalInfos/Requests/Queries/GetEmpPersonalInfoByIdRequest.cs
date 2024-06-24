using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPersonalInfos.Requests.Queries
{
    public class GetEmpPersonalInfoByIdRequest : IRequest<object>
    {
        public int Id { get; set; }
    }
}