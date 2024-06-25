using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpSpouseInfos.Requests.Commands
{
    public class DeleteEmpSpouseInfoCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }
}