using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Religion.Validators;
using Hrm.Application.Features.Religion.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Hrm.Application.DTOs.Religion.Validators;
using Hrm.Application.Features.Religion.Requests.Commands;

namespace Hrm.Application.Features.Religion.Handlers.Commands
{
    public class CreateReligionCommandHandler : IRequestHandler<CreateReligionCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateReligionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateReligionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateReligionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ReligionDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Religion = _mapper.Map<Hrm.Domain.Religion>(request.ReligionDto);

                Religion = await _unitOfWork.Repository<Hrm.Domain.Religion>().Add(Religion);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Religion.ReligionId;
            }

            return response;
        }

    }
}
