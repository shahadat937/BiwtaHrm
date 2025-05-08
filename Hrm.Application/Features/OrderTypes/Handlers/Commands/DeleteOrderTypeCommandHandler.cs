using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.OrderTypes.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OrderTypes.Handlers.Commands
{
    public class DeleteOrderTypeCommandHandler : IRequestHandler<DeleteOrderTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOrderTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteOrderTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var OrderType = await _unitOfWork.Repository<Hrm.Domain.OrderType>().Get(request.Id);

            if (OrderType == null)
                throw new NotFoundException(nameof(OrderType), request.Id);

            await _unitOfWork.Repository<Hrm.Domain.OrderType>().Delete(OrderType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = OrderType.Id;

            return response;
        }
    }
}