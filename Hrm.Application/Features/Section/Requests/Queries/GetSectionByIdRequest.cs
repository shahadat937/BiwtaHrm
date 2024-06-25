using Hrm.Application.DTOs.Section;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Section.Requests.Queries
{
    public class GetSectionByIdRequest : IRequest<SectionDto>
    {
        public int SectionId { get; set; }
    }
}
