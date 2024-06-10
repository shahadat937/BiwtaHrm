using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Scale.Requests.Queries
{
    public class GetSelectScaleRequest : IRequest<List<SelectedModel>>
    {
        public int GradeId { get; set; }
    }
}
