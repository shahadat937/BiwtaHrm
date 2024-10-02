using Hrm.Application.DTOs.EmpTrainingInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTrainingInfos.Requests.Queries
{
    public class GetEmpTrainingInfoByEmpIdRequest : IRequest<List<EmpTrainingInfoDto>>
    {
        public int Id { get; set; }
    }
}
