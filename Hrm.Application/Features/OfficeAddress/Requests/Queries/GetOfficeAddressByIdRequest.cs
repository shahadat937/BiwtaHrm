using Hrm.Application.DTOs.OfficeAddress;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeAddress.Requests.Queries
{
    public class GetOfficeAddressByIdRequest : IRequest<OfficeAddressDto>
    {
        public int OfficeAddressId { get; set; }
    }
}
