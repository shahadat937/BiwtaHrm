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
    public class UpdateFormSectionCommandHandler : IRequestHandler<UpdateFormSectionCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFormSectionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateFormSectionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new GetFormSectionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FormSection);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var FormSection = await _unitOfWork.Repository<Hrm.Domain.FormSection>().Get(request.FormSection.FormSectionId);

            if(FormSection == null)
            {
                throw new NotFoundException(nameof(FormSection),request.FormSection.FormSectionId);
            }

            _mapper.Map(request.FormSection, FormSection);

            await _unitOfWork.Repository<Hrm.Domain.FormSection>().Update(FormSection);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = FormSection.FormSectionId;

            return response;
        }
    }
}
