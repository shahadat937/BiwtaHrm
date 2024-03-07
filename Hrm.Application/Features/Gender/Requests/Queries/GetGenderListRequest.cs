using Hrm.Application.DTOs.Gender;
using Hrm.Application.Models;
using MediatR;
using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Gender.Requests.Queries
{
    public class GetGenderListRequest : IRequest<PagedResult<GenderDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
