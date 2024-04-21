using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.HairColors.Requests.Queries
{
    public class GetSelectedHairColorRequest : IRequest<List<SelectedModel>>
    {

    }
} 
      