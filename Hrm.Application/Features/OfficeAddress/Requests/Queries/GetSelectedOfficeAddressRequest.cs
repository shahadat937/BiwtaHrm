using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.OfficeAddress.Requests.Queries
{
    public class GetSelectedOfficeAddressRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      