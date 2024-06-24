using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPresentAddresses.Requests.Queries
{
    public class GetEmpPresentAddressByIdRequest : IRequest<object>
    {
        public int Id { get; set; }
    }
}
