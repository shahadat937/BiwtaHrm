using Hrm.Application.DTOs.Division;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Division.Requests.Queries
{
    public class GetDivisionByCountryIdRequest:IRequest<List<SelectedModel>>
    {
        public int CountryId { get; set; }
    }
}
