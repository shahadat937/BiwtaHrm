using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Ward.Request.Queries
{
    public class GetWardByUnionIdRequest : IRequest<List<SelectedModel>>
    {
        public int UnionId { get; set; }
    }
}
