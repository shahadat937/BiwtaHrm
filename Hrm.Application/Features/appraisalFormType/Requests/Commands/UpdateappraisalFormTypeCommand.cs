using Hrm.Application.DTOs.appraisalFormType;
using Hrm.Application.Responses;
using MediatR;
namespace Hrm.Application.Features.appraisalFormType.Requests.Commands
{
    public class UpdateappraisalFormTypeCommand : IRequest<BaseCommandResponse>
    {
        public required appraisalFormTypeDto appraisalFormTypeDto { get; set; }
    }
}
