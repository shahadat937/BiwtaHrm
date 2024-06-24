using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteSubjectCommand : IRequest
    {
        public int SubjectId { get; set; }
    }
}
