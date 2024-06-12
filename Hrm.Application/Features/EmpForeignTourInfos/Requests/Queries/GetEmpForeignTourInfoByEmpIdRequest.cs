using Hrm.Application.DTOs.EmpForeignTourInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpForeignTourInfos.Requests.Queries
{
    public class GetEmpForeignTourInfoByEmpIdRequest : IRequest<List<EmpForeignTourInfoDto>>
    {
        public int Id { get; set; }
    }
}
