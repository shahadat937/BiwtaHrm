using Hrm.Application.DTOs.Group;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Group.Requests.Queries
{
    public class GetGroupByIdRequest : IRequest<GroupDto>
    {
        public int GroupId { get; set; }
    }
}
