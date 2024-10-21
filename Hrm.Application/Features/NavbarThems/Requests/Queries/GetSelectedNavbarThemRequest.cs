using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.NavbarThems.Requests.Queries
{
    public class GetSelectedNavbarThemRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      