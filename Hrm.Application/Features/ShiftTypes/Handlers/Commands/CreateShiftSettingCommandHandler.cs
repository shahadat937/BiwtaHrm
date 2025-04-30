using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ShiftType;
using Hrm.Application.Features.ShiftTypes.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftTypes.Handlers.Commands
{
    public class CreateShiftTypeCommandHandler : IRequestHandler<CreateShiftTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateShiftTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateShiftTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var findActive = await _unitOfWork.Repository<ShiftType>().FindOneAsync(x => x.IsDefault == true);
            if (findActive != null && request.ShiftTypeDto.IsDefault == true)
            {
                findActive.IsDefault = false;
                await _unitOfWork.Repository<ShiftType>().Update(findActive);
            }


            var shiftTypes = _mapper.Map<ShiftType>(request.ShiftTypeDto);

            shiftTypes = await _unitOfWork.Repository<ShiftType>().Add(shiftTypes);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = shiftTypes.Id;

            return response;
        }
    }
}
