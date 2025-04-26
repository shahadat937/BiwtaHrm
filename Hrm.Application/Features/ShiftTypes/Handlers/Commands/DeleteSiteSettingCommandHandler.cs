using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ShiftTypes.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftTypes.Handlers.Commands
{
    public class DeleteShiftTypeCommandHandler : IRequestHandler<DeleteShiftTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShiftTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteShiftTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var ShiftType = await _unitOfWork.Repository<Hrm.Domain.ShiftType>().Get(request.Id);

            if (ShiftType == null)
                throw new NotFoundException(nameof(ShiftType), request.Id);

            await _unitOfWork.Repository<Hrm.Domain.ShiftType>().Delete(ShiftType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = ShiftType.Id;

            return response;
        }
    }
}