using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Country.Requests.Commands
{
    public class DeleteCountryCommand : IRequest<BaseCommandResponse>
    {
        public int CountryId { get; set; }
    }
}
