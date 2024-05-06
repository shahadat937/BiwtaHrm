using Hrm.Application.DTOs.SubDepartment;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubDepartment.Requests.Queries
{
    public class GetSubDepartmentByDepartmentIdRequest : IRequest<List<SelectedModel>>
    {
        public int DepartmentId { get; set; }
    }
}
