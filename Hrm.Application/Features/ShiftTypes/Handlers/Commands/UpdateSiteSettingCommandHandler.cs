using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ShiftTypes.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftTypes.Handlers.Commands
{
    public class UpdateShiftTypeCommandHandler : IRequestHandler<UpdateShiftTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShiftTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateShiftTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var ShiftType = await _unitOfWork.Repository<ShiftType>().Get(request.ShiftTypeDto.Id);


            var findActive = await _unitOfWork.Repository<ShiftType>().FindOneAsync(x => x.IsDefault == true);
            if (findActive != null && request.ShiftTypeDto.IsDefault == true)
            {
                findActive.IsActive = false;
                await _unitOfWork.Repository<ShiftType>().Update(findActive);
            }


            await _unitOfWork.Repository<ShiftType>().Update(ShiftType);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Update Successful";
            response.Id = ShiftType.Id;

            return response;
        }
    }
}