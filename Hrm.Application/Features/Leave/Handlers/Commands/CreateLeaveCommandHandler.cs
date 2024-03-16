using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Leave.Validators;
using Hrm.Application.Features.Leave.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Hrm.Application.DTOs.Leave.Validators;
using Hrm.Application.Features.Leave.Requests.Commands;

namespace Hrm.Application.Features.Leave.Handlers.Commands
{
    public class CreateLeaveCommandHandler : IRequestHandler<CreateLeaveCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateLeaveCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Leave = _mapper.Map<Hrm.Domain.Leave>(request.LeaveDto);

                Leave = await _unitOfWork.Repository<Hrm.Domain.Leave>().Add(Leave);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Leave.LeaveId;
            }

            return response;
        }

    }
}
