using Hrm.Application.DTOs.EmpBankInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBankInfos.Requests.Queries
{
    public class GetEmpBankInfoByEmpIdRequest : IRequest<List<EmpBankInfoDto>>
    {
        public int Id { get; set; }
    }
}
