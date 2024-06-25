using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Shift.Validators;
using Hrm.Application.Features.Shift.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Hrm.Application.DTOs.Shift.Validators;
using Hrm.Application.Features.Shift.Requests.Commands;

namespace Hrm.Application.Features.Shift.Handlers.Commands
{
    public class CreateShiftCommandHandler : IRequestHandler<CreateShiftCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateShiftCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateShiftDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ShiftDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Shift = _mapper.Map<Hrm.Domain.Shift>(request.ShiftDto);

                Shift = await _unitOfWork.Repository<Hrm.Domain.Shift>().Add(Shift);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Shift.ShiftId;
            }

            return response;
        }

    }
}
