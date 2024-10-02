using Hrm.Application.DTOs.EmpWorkHistory;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpWorkHistories.Requests.Queries
{
    public class GetEmpWorkHistoryByEmpIdRequest : IRequest<List<EmpWorkHistoryDto>>
    {
        public int Id { get; set; }
    }
}
