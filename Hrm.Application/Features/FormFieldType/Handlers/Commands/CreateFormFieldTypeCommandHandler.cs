using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormFieldType.Validators;
using Hrm.Application.Features.FormFieldType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormFieldType.Handlers.Commands
{
    internal class CreateFormFieldTypeCommandHandler : IRequestHandler<CreateFormFieldTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFormFieldTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFormFieldTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFormFieldTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.formFieldTypeDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var formFieldType = _mapper.Map<Hrm.Domain.FormFieldType>(request.formFieldTypeDto);
            await _unitOfWork.Repository<Hrm.Domain.FormFieldType>().Add(formFieldType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = formFieldType.FieldTypeId;

            return response;
        }
    }
}
