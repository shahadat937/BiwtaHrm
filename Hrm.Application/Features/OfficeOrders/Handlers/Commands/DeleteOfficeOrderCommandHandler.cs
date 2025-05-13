using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.OfficeOrders.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeOrders.Handlers.Commands
{
    public class DeleteOfficeOrderCommandHandler : IRequestHandler<DeleteOfficeOrderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOfficeOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteOfficeOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var OfficeOrder = await _unitOfWork.Repository<Hrm.Domain.OfficeOrder>().Get(request.Id);

            if (OfficeOrder == null)
                throw new NotFoundException(nameof(OfficeOrder), request.Id);

            if (!string.IsNullOrEmpty(OfficeOrder.FileUrl))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\OfficeOrder", OfficeOrder.FileUrl);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            await _unitOfWork.Repository<Hrm.Domain.OfficeOrder>().Delete(OfficeOrder);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = OfficeOrder.Id;

            return response;
        }
    }
}