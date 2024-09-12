using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Requests.Queries
{
    public class GetDesignationLastPositionRequest : IRequest<int>
    {
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
    }
}
