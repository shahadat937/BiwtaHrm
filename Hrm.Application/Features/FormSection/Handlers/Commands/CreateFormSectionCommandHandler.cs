using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormSection.Validators;
using Hrm.Application.Features.FormSection.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;

using MediatR;

namespace Hrm.Application.Features.FormSection.Handlers.Commands
{
    public class CreateFormSectionCommandHandler : IRequestHandler<CreateFormSectionCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFormSectionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFormSectionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFormSectionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FormSection);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var FormSection = _mapper.Map<Hrm.Domain.FormSection>(request.FormSection);

            await _unitOfWork.Repository<Hrm.Domain.FormSection>().Add(FormSection);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = FormSection.FormSectionId;

            return response;
        }
    }
}
