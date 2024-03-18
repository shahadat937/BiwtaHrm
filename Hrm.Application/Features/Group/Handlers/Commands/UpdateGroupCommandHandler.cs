using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Group.Validators;
using Hrm.Application.DTOs.Group.ValidatorsGroup;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Group.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Group.Handlers.Commands
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateGroupDtoValidator();
            var validationResult = await validator.ValidateAsync(request.GroupDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Group = await _unitOfWork.Repository<Hrm.Domain.Group>().Get(request.GroupDto.GroupId);

            if (Group is null)
            {
                throw new NotFoundException(nameof(Group), request.GroupDto.GroupId);
            }

            _mapper.Map(request.GroupDto, Group);

            await _unitOfWork.Repository<Hrm.Domain.Group>().Update(Group);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
