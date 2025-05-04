using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ShiftSettings.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftSettings.Handlers.Commands
{
    public class UpdateShiftSettingCommandHandler : IRequestHandler<UpdateShiftSettingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShiftSettingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateShiftSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var ShiftSetting = await _unitOfWork.Repository<ShiftSetting>().Get(request.ShiftSettingDto.Id);


            var findActive = await _unitOfWork.Repository<ShiftSetting>().FindOneAsync(x => x.IsActive == true && x.ShiftTypeId == request.ShiftSettingDto.ShiftTypeId);
            if (findActive != null && request.ShiftSettingDto.IsActive == true)
            {
                findActive.IsActive = false;
                await _unitOfWork.Repository<ShiftSetting>().Update(findActive);
            }

            var shiftSettingDto = _mapper.Map(request.ShiftSettingDto, ShiftSetting);


            await _unitOfWork.Repository<ShiftSetting>().Update(shiftSettingDto);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Update Successful";
            response.Id = ShiftSetting.Id;

            return response;
        }
    }
}