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
    public class UpdateFormFieldTypeCommandHandler: IRequestHandler<UpdateFormFieldTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFormFieldTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateFormFieldTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateFormFieldTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FormFieldTypeDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var formFieldType = await _unitOfWork.Repository<Hrm.Domain.FormFieldType>().Get(request.FormFieldTypeDto.FieldTypeId);

            if(formFieldType==null)
            {
                throw new NotFoundException(nameof(formFieldType),request.FormFieldTypeDto.FieldTypeId);
            }

            _mapper.Map(request.FormFieldTypeDto, formFieldType);

            await _unitOfWork.Repository<Hrm.Domain.FormFieldType>().Update(formFieldType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = formFieldType.FieldTypeId;

            return response;
        }
    }
}
