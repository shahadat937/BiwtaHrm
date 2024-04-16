using Hrm.Application.DTOs.Relation;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Relation.Requests.Commands
{
    public class UpdateRelationCommand : IRequest<BaseCommandResponse>
    {
        public RelationDto RelationDto { get; set; }
    }
}
