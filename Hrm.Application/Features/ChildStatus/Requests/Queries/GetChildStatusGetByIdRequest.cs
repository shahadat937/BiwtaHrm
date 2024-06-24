
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ChildStatus;

namespace Hrm.Application.Features.ChildStatus.Requests.Queries
{
    public class GetChildStatusGetByIdRequest : IRequest<ChildStatusDto>
    {
        public int ChildStatusId { get; set; }
    }
}
