using Hrm.Application.DTOs.DepReleaseInfo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DepReleaseInfo.Requests.Queries
{
    public class GetDepReleaseInfoByIdRequest : IRequest<DepReleaseInfoDto>
    {
        public int DepReleaseInfoId { get; set; }
    }
}
