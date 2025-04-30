using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ShiftSettings.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftSettings.Handlers.Commands
{
    public class DeleteShiftSettingCommandHandler : IRequestHandler<DeleteShiftSettingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShiftSettingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteShiftSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var ShiftSetting = await _unitOfWork.Repository<Hrm.Domain.ShiftSetting>().Get(request.Id);

            if (ShiftSetting == null)
                throw new NotFoundException(nameof(ShiftSetting), request.Id);

            await _unitOfWork.Repository<Hrm.Domain.ShiftSetting>().Delete(ShiftSetting);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = ShiftSetting.Id;

            return response;
        }
    }
}