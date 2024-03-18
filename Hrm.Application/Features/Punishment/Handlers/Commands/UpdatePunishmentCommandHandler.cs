using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Punishment.Validators;
using Hrm.Application.Exceptions;
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
    public class UpdatePunishmentCommandHandler : IRequestHandler<UpdatePunishmentCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePunishmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePunishmentCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdatePunishmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PunishmentDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Punishment = await _unitOfWork.Repository<Hrm.Domain.Punishment>().Get(request.PunishmentDto.PunishmentId);

            if (Punishment is null)
            {
                throw new NotFoundException(nameof(Punishment), request.PunishmentDto.PunishmentId);
            }

            _mapper.Map(request.PunishmentDto, Punishment);

            await _unitOfWork.Repository<Hrm.Domain.Punishment>().Update(Punishment);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
