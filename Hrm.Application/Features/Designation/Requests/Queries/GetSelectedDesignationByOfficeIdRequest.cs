using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Requests.Queries
{
    public class GetSelectedDesignationByOfficeIdRequest : IRequest<List<SelectedModel>>
    {
        public int OfficeId { get; set; }
        public int EmpJobDetailId { get; set; }
    }
}
