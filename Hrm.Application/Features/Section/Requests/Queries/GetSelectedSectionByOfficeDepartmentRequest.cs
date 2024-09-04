using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Section.Requests.Queries
{
    public class GetSelectedSectionByOfficeDepartmentRequest : IRequest<List<SelectedModel>>
    {
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
