using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TaskName.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.TaskName.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TaskName.Handlers.Commands
{
    public class UpdateTaskNameCommandHandler : IRequestHandler<UpdateTaskNameCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTaskNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTaskNameCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateTaskNameDtoValidator();
            var validationResult = await validator.ValidateAsync(request.TaskNameDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var TaskName = await _unitOfWork.Repository<Hrm.Domain.TaskName>().Get(request.TaskNameDto.TaskNameId);

            if (TaskName is null)
            {
                throw new NotFoundException(nameof(TaskName), request.TaskNameDto.TaskNameId);
            }

            _mapper.Map(request.TaskNameDto, TaskName);

            await _unitOfWork.Repository<Hrm.Domain.TaskName>().Update(TaskName);
            await _unitOfWork.Save();

            return Unit.Value;
        }

    }
}
