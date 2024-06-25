using Hrm.Application.DTOs.TrainingName;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingName.Requests.Commands
{
    public class CreateTrainingNameCommand : IRequest<BaseCommandResponse>
    {
        public CreateTrainingNameDto TrainingNameDto { get; set; }
    }
}
