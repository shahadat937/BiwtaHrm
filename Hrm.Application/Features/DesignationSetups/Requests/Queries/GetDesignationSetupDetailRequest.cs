
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.DesignationSetup;

namespace Hrm.Application.Features.DesignationSetups.Requests.Queries
{
    public class GetDesignationSetupDetailRequest : IRequest<DesignationSetupDto>
    {
        public int DesignationSetupId { get; set; }
    }
}
