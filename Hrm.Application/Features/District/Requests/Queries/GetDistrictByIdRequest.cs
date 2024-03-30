using Hrm.Application.DTOs.District;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.District.Requests.Queries
{
    public class GetDistrictByIdRequest : IRequest<DistrictDto>
    {
        public int DistrictId { get; set; }
    }
}
