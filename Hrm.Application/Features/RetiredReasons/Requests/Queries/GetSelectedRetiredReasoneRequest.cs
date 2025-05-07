using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.RetiredReasons.Requests.Queries
{
    public class GetSelectedRetiredReasonRequest : IRequest<List<SelectedModel>>
    {
    }
}
