using Hrm.Application.DTOs.EmpJobDetail;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpJobDetails.Requests.Queries
{
    public class GetEmpDepartmentSectionDesignationRequest: IRequest<List<EmpDepatmentSectionAndDesignationInfoDto>>
    {
        public int? EmpId { get; set; }
    }
}
