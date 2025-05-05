using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ShiftSetting;
using Hrm.Application.Features.ShiftSettings.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftSettings.Handlers.Commands
{
    public class CreateShiftSettingCommandHandler : IRequestHandler<CreateShiftSettingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateShiftSettingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateShiftSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var findActive = await _unitOfWork.Repository<ShiftSetting>().FindOneAsync(x => x.IsActive == true && x.ShiftTypeId == request.ShiftSettingDto.ShiftTypeId);
            if (findActive != null && request.ShiftSettingDto.IsActive == true)
            {
                findActive.IsActive = false;
                await _unitOfWork.Repository<ShiftSetting>().Update(findActive);
            }


            var ShiftSettings = _mapper.Map<ShiftSetting>(request.ShiftSettingDto);

            ShiftSettings = await _unitOfWork.Repository<ShiftSetting>().Add(ShiftSettings);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = ShiftSettings.Id;

            return response;
        }
    }
}
