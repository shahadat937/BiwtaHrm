using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Relation.Requests.Queries
{
    public class GetSelectRelationRequest : IRequest<List<SelectedModel>>
    {
    }
}
