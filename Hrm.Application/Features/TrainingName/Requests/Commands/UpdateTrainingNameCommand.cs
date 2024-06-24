using Hrm.Application.DTOs.TrainingName;
using Hrm.Application.Responses;
using MediatR;
namespace Hrm.Application.Features.TrainingName.Requests.Commands
{
    public class UpdateTrainingNameCommand : IRequest<BaseCommandResponse>
    {
        public required TrainingNameDto TrainingNameDto { get; set; }
    }
}
