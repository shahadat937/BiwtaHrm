using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingName.Requests.Commands
{
    public class DeleteTrainingNameCommand : IRequest<BaseCommandResponse>
    {
        public int TrainingNameId { get; set; }
    }
}
