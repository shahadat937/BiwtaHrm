using Hrm.Application.DTOs.Organograms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Organogram.Requests.Queries
{
    public class GetOrganogramRequest : IRequest<OrganogramOfficeDto>
    {
    }
}
