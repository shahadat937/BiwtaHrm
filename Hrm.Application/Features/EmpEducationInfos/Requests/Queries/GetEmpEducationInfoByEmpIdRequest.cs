using Hrm.Application.DTOs.EmpEducationInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpEducationInfos.Requests.Queries
{
    public class GetEmpEducationInfoByEmpIdRequest : IRequest<List<EmpEducationInfoDto>>
    {
        public int Id { get; set; }
    }
}
