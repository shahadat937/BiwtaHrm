using Hrm.Application.DTOs.EmpOtherResponsibility;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpOtherResponsibilities.Requests.Queries
{
    public class GetEmpOtherResponsibilityByEmpIdRequest : IRequest<List<EmpOtherResponsibilityDto>>
    {
        public int Id { get; set; }
    }
}
