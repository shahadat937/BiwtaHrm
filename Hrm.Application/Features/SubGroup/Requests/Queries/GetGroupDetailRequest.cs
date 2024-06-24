
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Group;

namespace Hrm.Application.Features.Groups.Requests.Queries
{
    public class GetGroupDetailRequest : IRequest<GroupDto>
    {
        public int GroupId { get; set; }
    }
}
