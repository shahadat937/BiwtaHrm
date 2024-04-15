using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Punishments.Requests.Queries
{
    public class GetSelectedPunishmentRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      