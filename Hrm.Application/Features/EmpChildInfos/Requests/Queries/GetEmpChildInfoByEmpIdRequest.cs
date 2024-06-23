using Hrm.Application.DTOs.EmpChildInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpChildInfos.Requests.Queries
{
    public class GetEmpChildInfoByEmpIdRequest : IRequest<List<EmpChildInfoDto>>
    {
        public int Id { get; set; }
    }
}
