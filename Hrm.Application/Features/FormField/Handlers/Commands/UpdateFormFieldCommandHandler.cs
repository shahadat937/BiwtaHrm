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
    public class UpdateFormFieldCommandHandler: IRequestHandler<UpdateFormFieldCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFormFieldCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateFormFieldCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateFormFieldDtoValidator();
            var validationResult = await validator.ValidateAsync(request.formFieldDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var formField = await _unitOfWork.Repository<Hrm.Domain.FormField>().Get(request.formFieldDto.FieldId);

            if(formField == null)
            {
                throw new NotFoundException(nameof(formField),request.formFieldDto.FieldId);
            }

            _mapper.Map(request.formFieldDto, formField);

            await _unitOfWork.Repository<Hrm.Domain.FormField>().Update(formField);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = formField.FieldId;

            return response;
        }
    }
}
