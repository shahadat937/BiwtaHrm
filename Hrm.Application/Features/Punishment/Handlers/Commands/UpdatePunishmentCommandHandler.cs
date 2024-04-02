using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Punishment.Validators;
using Hrm.Application.DTOs.Punishment.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Punishment.Requests.Commands;
using Hrm.Application.Features.Punishment.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Punishment.Handlers.Commands
{
    public class UpdatePunishmentCommandHandler : IRequestHandler<UpdatePunishmentCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Punishment> _PunishmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdatePunishmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Punishment> PunishmentRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _PunishmentRepository = PunishmentRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdatePunishmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new IPunishmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PunishmentDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Punishment = await _unitOfWork.Repository<Hrm.Domain.Punishment>().Get(request.PunishmentDto.PunishmentId);

            if (Punishment is null)
            {
                throw new NotFoundException(nameof(Punishment), request.PunishmentDto.PunishmentId);
            }

            var PunishmentName = request.PunishmentDto.PunishmentName.ToLower();

            IQueryable<Hrm.Domain.Punishment> Punishments = _PunishmentRepository.Where(x => x.PunishmentName.ToLower() == PunishmentName);


            if (Punishments.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.PunishmentDto.PunishmentName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.PunishmentDto, Punishment);

                await _unitOfWork.Repository<Hrm.Domain.Punishment>().Update(Punishment);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = Punishment.PunishmentId;

            }
            return response;
        }
    }
}
