﻿using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteVisit.Requests.Commands
{
    public class DeclineSiteVisitCommand: IRequest<BaseCommandResponse>
    {
        public int SiteVisitId { get; set; }
    }
}
