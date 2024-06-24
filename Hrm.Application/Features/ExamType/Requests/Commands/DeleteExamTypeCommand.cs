using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteExamTypeCommand : IRequest
    {
        public int ExamTypeId { get; set; }
    }
}
