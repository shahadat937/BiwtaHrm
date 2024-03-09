using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmployeeType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.EmployeeType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmployeeType.Handlers.Commands
{
    public class UpdateEmployeeTypeCommandHandler : IRequestHandler<UpdateEmployeeTypeCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmployeeTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEmployeeTypeCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateEmployeeTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EmployeeTypeDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var EmployeeType = await _unitOfWork.Repository<Hrm.Domain.EmployeeType>().Get(request.EmployeeTypeDto.EmployeeTypeId);

            if (EmployeeType is null)
            {
                throw new NotFoundException(nameof(EmployeeType), request.EmployeeTypeDto.EmployeeTypeId);
            }

            _mapper.Map(request.EmployeeTypeDto, EmployeeType);

            await _unitOfWork.Repository<Hrm.Domain.EmployeeType>().Update(EmployeeType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
