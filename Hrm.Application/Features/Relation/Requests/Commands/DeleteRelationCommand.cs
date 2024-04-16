using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Relation.Requests.Commands
{
    public class DeleteRelationCommand : IRequest<BaseCommandResponse>
    {
        public int RelationId { get; set; }
    }
}
