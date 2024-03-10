using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingType.Requests.Commands
{
    public class DeleteTrainingTypeCommand : IRequest<BaseCommandResponse>
    {
        public int TrainingTypeId { get; set; }
    }
}
