using Hrm.Application.DTOs.EmpNomineeInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpNomineeInfos.Requests.Queries
{
    public class GetEmpNomineeInfoByEmpIdRequest : IRequest<List<EmpNomineeInfoDto>>
    {
        public int Id { get; set; }
    }
}
