using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Requests.Queries
{
    public class GetDesignationBySectionIdRequest : IRequest<List<SelectedModel>>
    {
        public int SectionId { get; set; }
    }
}

