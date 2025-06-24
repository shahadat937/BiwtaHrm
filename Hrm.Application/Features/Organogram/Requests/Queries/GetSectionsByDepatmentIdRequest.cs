using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.Organograms;
using Hrm.Application.DTOs.Section;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Organogram.Requests.Queries
{
    public class GetSectionsByDepatmentIdRequest : IRequest<List<SectionDto>>
    {
        public int? DepartmentId { get; set; }
        public int?  UpperSectionId { get; set; }
    }
}
