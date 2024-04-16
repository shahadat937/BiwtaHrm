using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TaskName.Validators;
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
    public class CreateTaskNameCommandHandler : IRequestHandler<CreateTaskNameCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateTaskNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateTaskNameCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateTaskNameDtoValidator();
            var validationResult = await validator.ValidateAsync(request.TaskNameDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var TaskName = _mapper.Map<Hrm.Domain.TaskName>(request.TaskNameDto);

                TaskName = await _unitOfWork.Repository<Hrm.Domain.TaskName>().Add(TaskName);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = TaskName.TaskNameId;
            }

            return response;
        }

    }

}
