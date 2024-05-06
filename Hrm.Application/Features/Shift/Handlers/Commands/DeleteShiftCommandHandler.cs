using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;
using Hrm.Application.Responses;

namespace hrm.Application.Features.Shifts.Handlers.Commands
{
    public class DeleteShiftCommandHandler : IRequestHandler<DeleteShiftCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShiftCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
        {

            var response = new BaseCommandResponse();

            var Shift = await _unitOfWork.Repository<Shift>().Get(request.ShiftId);

            if (Shift == null)
            {
                response.Success = false;
                response.Message = "Creation Failed";
            }

            await _unitOfWork.Repository<Shift>().Delete(Shift);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Deleted Successfull";


            return response;
        }
    }
}
