using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Punishment.Validators;
using Hrm.Application.Features.Punishment.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.Punishment.Handlers.Commands
{
    public class CreatePunishmentCommandHandler : IRequestHandler<CreatePunishmentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePunishmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreatePunishmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreatePunishmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PunishmentDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Punishment = _mapper.Map<Hrm.Domain.Punishment>(request.PunishmentDto);

                Punishment = await _unitOfWork.Repository<Hrm.Domain.Punishment>().Add(Punishment);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Punishment.PunishmentId;
            }

            return response;
        }

    }
}
