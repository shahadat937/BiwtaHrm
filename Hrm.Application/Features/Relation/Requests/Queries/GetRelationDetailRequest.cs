
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Relation;

namespace Hrm.Application.Features.Relations.Requests.Queries
{
    public class GetRelationDetailRequest : IRequest<RelationDto>
    {
        public int RelationId { get; set; }
    }
}
