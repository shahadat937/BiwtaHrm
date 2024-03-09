using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.MaritalStatus.Handlers.Commands
{
    public class UpdateMaritalStatusCommandHandler : IRequestHandler<UpdateMaritalStatusCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMaritalStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMaritalStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateMaritalStatusDtoValidator();
            var validationResult = await validator.ValidateAsync(request.MaritalStatusDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var MaritalStatus = await _unitOfWork.Repository<Hrm.Domain.MaritalStatus>().Get(request.MaritalStatusDto.MaritalStatusId);

            if (MaritalStatus is null)
            {
                throw new NotFoundException(nameof(MaritalStatus), request.MaritalStatusDto.MaritalStatusId);
            }

            _mapper.Map(request.MaritalStatusDto, MaritalStatus);

            await _unitOfWork.Repository<Hrm.Domain.MaritalStatus>().Update(MaritalStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
