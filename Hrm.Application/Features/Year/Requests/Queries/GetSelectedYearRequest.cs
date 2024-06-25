using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Year.Requests.Queries
{
    public class GetSelectedYearRequest : IRequest<List<SelectedModel>>
    {

    }
} 
      