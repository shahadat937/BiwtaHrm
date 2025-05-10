using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.OrderTypes.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OrderTypes.Handlers.Commands
{
    public class UpdateOrderTypeCommandHandler : IRequestHandler<UpdateOrderTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateOrderTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var OrderType = await _unitOfWork.Repository<OrderType>().Get(request.OrderTypeDto.Id);


            var OrderTypesDto = _mapper.Map(request.OrderTypeDto, OrderType);


            await _unitOfWork.Repository<OrderType>().Update(OrderTypesDto);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Update Successful";
            response.Id = OrderType.Id;

            return response;
        }
    }
}