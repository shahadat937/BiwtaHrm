using Hrm.Application.DTOs.EmpSpouseInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpSpouseInfos.Requests.Queries
{
    public class GetEmpSpouseInfoByEmpIdRequest : IRequest<List<EmpSpouseInfoDto>>
    {
        public int Id { get; set; }
    }
}
