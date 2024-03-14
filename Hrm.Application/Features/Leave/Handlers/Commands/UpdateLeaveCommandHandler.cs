using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Leave.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Leave.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Leave.Handlers.Commands
{
    public class UpdateLeaveCommandHandler : IRequestHandler<UpdateLeaveCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateLeaveDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Leave = await _unitOfWork.Repository<Hrm.Domain.Leave>().Get(request.LeaveDto.LeaveId);

            if (Leave is null)
            {
                throw new NotFoundException(nameof(Leave), request.LeaveDto.LeaveId);
            }

            _mapper.Map(request.LeaveDto, Leave);

            await _unitOfWork.Repository<Hrm.Domain.Leave>().Update(Leave);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
