using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpJobDetails.Requests.Queries
{
    public class GetSelectedDepartmentByEmpIdRequest : IRequest<List<SelectedModel>>
    {
        public int? EmpId { get; set; }
    }
}
