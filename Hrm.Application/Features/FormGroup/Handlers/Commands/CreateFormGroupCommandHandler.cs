using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormGroup.Validators;
using Hrm.Application.Features.FormGroup.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;

namespace Hrm.Application.Features.FormGroup.Handlers.Commands
{
    public class CreateFormGroupCommandHandler : IRequestHandler<CreateFormGroupCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFormGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFormGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFormGroupDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FormGroup);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var formGroup = _mapper.Map<Hrm.Domain.FormGroup>(request.FormGroup);

            await _unitOfWork.Repository<Hrm.Domain.FormGroup>().Add(formGroup);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = formGroup.FormGroupId;

            return response;
        }
    }
}
