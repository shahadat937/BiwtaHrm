using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Language.Requests.Queries
{
    public class GetSelectedLanguageRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      