using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ChildStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ChildStatus.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ChildStatus.Handlers.Commands
{
    public class UpdateChildStatusCommandHandler : IRequestHandler<UpdateChildStatusCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateChildStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateChildStatusCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateChildStatusDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ChildStatusDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var ChildStatus = await _unitOfWork.Repository<Hrm.Domain.ChildStatus>().Get(request.ChildStatusDto.ChildStatusId);

            if (ChildStatus is null)
            {
                throw new NotFoundException(nameof(ChildStatus), request.ChildStatusDto.ChildStatusId);
            }

            _mapper.Map(request.ChildStatusDto, ChildStatus);

            await _unitOfWork.Repository<Hrm.Domain.ChildStatus>().Update(ChildStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
