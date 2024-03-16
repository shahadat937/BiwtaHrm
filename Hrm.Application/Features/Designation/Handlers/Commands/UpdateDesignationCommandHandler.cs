using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Designation.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Designation.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Handlers.Commands
{
    public class UpdateDesignationCommandHandler : IRequestHandler<UpdateDesignationCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDesignationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDesignationCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateDesignationDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DesignationDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Designation = await _unitOfWork.Repository<Hrm.Domain.Designation>().Get(request.DesignationDto.DesignationId);

            if (Designation is null)
            {
                throw new NotFoundException(nameof(Designation), request.DesignationDto.DesignationId);
            }

            _mapper.Map(request.DesignationDto, Designation);

            await _unitOfWork.Repository<Hrm.Domain.Designation>().Update(Designation);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
