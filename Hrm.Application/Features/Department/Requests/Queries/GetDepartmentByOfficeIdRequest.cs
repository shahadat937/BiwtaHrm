using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeBranch.Requests.Queries
{
    public class GetDepartmentByOfficeIdRequest : IRequest<List<SelectedModel>>
    {
        public int OfficeId { get; set; }
    }
}
