using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Section.Requests.Queries
{
    public class GetSectionLastSequenceRequest : IRequest<int>
    {
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
    }
}