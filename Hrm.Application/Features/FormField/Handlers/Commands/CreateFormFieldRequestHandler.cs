using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormField.Validators;
using Hrm.Application.Features.FormField.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormField.Handlers.Commands
{
    public class CreateFormFieldRequestHandler : IRequestHandler<CreateFormFieldCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFormFieldRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFormFieldCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFormFieldDtoValidator();
            var validationResult = await validator.ValidateAsync(request.formFieldDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var formField = _mapper.Map<Hrm.Domain.FormField>(request.formFieldDto);

            await _unitOfWork.Repository<Hrm.Domain.FormField>().Add(formField);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = formField.FieldId;

            return response;
        }
    }
}
