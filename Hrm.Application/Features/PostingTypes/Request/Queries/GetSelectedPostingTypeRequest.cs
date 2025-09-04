using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.PostingTypes.Request.Queries
{
    public class GetSelectedPostingTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      