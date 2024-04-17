using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Countrys.Requests.Queries
{
    public class GetSelectedCountryRequest : IRequest<List<SelectedModel>>
    {

    }
} 
      