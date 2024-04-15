using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.PromotionTypes.Requests.Queries
{
    public class GetSelectedPromotionTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      