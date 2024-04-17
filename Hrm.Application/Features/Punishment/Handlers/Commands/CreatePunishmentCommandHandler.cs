using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Punishment.Validators;
using Hrm.Application.DTOs.Punishment.Validators;
using Hrm.Application.Features.Punishment.Requests.Commands;
using Hrm.Application.Features.Punishment.Requests.Commands;
using Hrm.Application.Features.Punishment.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.Punishment.Handlers.Commands
{
    public class CreatePunishmentCommandHandler : IRequestHandler<CreatePunishmentCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Punishment> _PunishmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePunishmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Punishment> PunishmentRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _PunishmentRepository = PunishmentRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreatePunishmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreatePunishmentDtoValidator();
            var validationPunishment = await validator.ValidateAsync(request.PunishmentDto);

            if (validationPunishment.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationPunishment.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var PunishmentName = request.PunishmentDto.PunishmentName.ToLower();

                IQueryable<Hrm.Domain.Punishment> Punishments = _PunishmentRepository.Where(x => x.PunishmentName.ToLower() == PunishmentName);


                if (PunishmentNameExists(request))
                {
                    response.Success = false;
                    //response.Message = "Creation Failed Name already exists.";
                    response.Message = $"Creation Failed '{request.PunishmentDto.PunishmentName}' already exists.";

                    response.Errors = validationPunishment.Errors.Select(q => q.ErrorMessage).ToList();

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
            }

            return response;
        }
        private bool PunishmentNameExists(CreatePunishmentCommand request)
        {
            var PunishmentName = request.PunishmentDto.PunishmentName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Punishment> Punishments = _PunishmentRepository.Where(x => x.PunishmentName.Trim().ToLower().Replace(" ", string.Empty) == PunishmentName);

            return Punishments.Any();
        }

    }
}

