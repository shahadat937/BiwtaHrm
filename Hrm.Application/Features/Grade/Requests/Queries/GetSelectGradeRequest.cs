using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Grade.Requests.Queries
{
    public class GetSelectGradeRequest : IRequest<List<SelectedModel>>
    {
    }
}
