using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Country.Requests.Queries
{
    public class GetDefaultCountryIdRequest : IRequest<int>
    {
    }
}
