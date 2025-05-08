using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.OrderType;
using Hrm.Application.Features.OrderTypes.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OrderTypes.Handlers.Commands
{
    public class CreateOrderTypeCommandHandler : IRequestHandler<CreateOrderTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateOrderTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var OrderTypes = _mapper.Map<OrderType>(request.OrderTypeDto);

            OrderTypes = await _unitOfWork.Repository<OrderType>().Add(OrderTypes);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = OrderTypes.Id;

            return response;
        }
    }
}
