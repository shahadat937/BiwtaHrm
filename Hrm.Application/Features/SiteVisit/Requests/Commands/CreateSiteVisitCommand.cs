using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.SiteVisit;
using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.SiteVisit.Requests.Commands
{
    public class CreateSiteVisitCommand: IRequest<BaseCommandResponse>
    {
        public CreateSiteVisitDto SiteVisitDto { get; set; }
    }

}
