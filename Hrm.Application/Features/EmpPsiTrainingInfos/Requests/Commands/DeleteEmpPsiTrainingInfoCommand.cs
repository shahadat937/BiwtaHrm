using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPsiTrainingInfos.Requests.Commands
{
    public class DeleteEmpPsiTrainingInfoCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }
}