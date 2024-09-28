using Hrm.Application.DTOs.EmpOtherResponsibility;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpOtherResponsibilities.Requests.Queries
{
    public class GetAllEmpOtherResponsibilityByEmpIdRequest : IRequest<List<EmpOtherResponsibilityDto>>
    {
        public int Id { get; set; }
    }
}
