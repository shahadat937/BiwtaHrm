using Hrm.Application.DTOs.EmpLanguageInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpLanguageInfos.Requests.Queries
{
    public class GetEmpLanguageInfoByEmpIdRequest : IRequest<List<EmpLanguageInfoDto>>
    {
        public int Id { get; set; }
    }
}
