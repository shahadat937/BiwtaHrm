using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteLanguageCommand : IRequest
    {
        public int LanguageId { get; set; }
    }
}
