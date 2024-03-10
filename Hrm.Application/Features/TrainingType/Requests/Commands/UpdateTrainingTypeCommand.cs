using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingType.Requests.Commands
{
    public class UpdateTrainingTypeCommand : IRequest<BaseCommandResponse>
    {
        public required TrainingTypeDto TrainingTypeDto { get; set; } 
    }
}
