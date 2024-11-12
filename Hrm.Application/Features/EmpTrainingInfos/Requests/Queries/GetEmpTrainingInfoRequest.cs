using Hrm.Application.DTOs.EmpTrainingInfo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTrainingInfos.Requests.Queries
{
    public class GetEmpTrainingInfoRequest: IRequest<object>
    {
        public EmpTrainingFilterDto Filters { get; set; }
    }
}
