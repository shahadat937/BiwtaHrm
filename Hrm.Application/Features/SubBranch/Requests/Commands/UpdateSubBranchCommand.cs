using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.Responses;
using MediatR;
namespace Hrm.Application.Features.SubBranch.Requests.Commands
{
    public class UpdateSubBranchCommand : IRequest<BaseCommandResponse>
    {
        public required SubBranchDto SubBranchDto { get; set; }
    }
}
