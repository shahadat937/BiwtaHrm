using Hrm.Application.DTOs.Section;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Section.Requests.Commands
{
    public class UpdateSectionCommand : IRequest<BaseCommandResponse>
    {
        public SectionDto SectionDto { get; set; }
    }
}
