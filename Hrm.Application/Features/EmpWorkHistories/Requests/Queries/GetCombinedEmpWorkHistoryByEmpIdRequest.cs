﻿using Hrm.Application.DTOs.EmpWorkHistory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpWorkHistories.Requests.Queries
{
    public class GetCombinedEmpWorkHistoryByEmpIdRequest : IRequest<List<EmpWorkHistoryDto>>
    {
        public int EmpId { get; set; }
    }
}
