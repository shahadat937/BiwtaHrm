
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Country;

namespace Hrm.Application.Features.Countrys.Requests.Queries
{
    public class GetCountryDetailRequest : IRequest<CountryDto>
    {
        public int CountryId { get; set; }
    }
}
