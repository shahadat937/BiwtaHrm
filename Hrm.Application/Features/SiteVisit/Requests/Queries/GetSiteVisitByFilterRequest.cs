﻿using Hrm.Application.DTOs.SiteVisit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteVisit.Requests.Queries
{
    public class GetSiteVisitByFilterRequest: IRequest<List<SiteVisitDto>>
    {
        public SiteVisitFilterDto filters {  get; set; }
    }
}
