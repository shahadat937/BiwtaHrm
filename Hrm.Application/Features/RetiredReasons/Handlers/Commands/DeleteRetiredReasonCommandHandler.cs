using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.RetiredReasons.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RetiredReasons.Handlers.Commands
{
    public class DeleteRetiredReasonCommandHandler : IRequestHandler<DeleteRetiredReasonCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRetiredReasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteRetiredReasonCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var RetiredReason = await _unitOfWork.Repository<Hrm.Domain.RetiredReason>().Get(request.Id);

            if (RetiredReason == null)
                throw new NotFoundException(nameof(RetiredReason), request.Id);

            await _unitOfWork.Repository<Hrm.Domain.RetiredReason>().Delete(RetiredReason);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = RetiredReason.Id;

            return response;
        }
    }
}