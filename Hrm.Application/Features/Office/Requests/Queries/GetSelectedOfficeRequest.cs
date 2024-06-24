using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Office.Requests.Queries
{
    public class GetSelectedOfficeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      