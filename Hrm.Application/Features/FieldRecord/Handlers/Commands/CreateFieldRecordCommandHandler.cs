using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FieldRecord.Validators;
using Hrm.Application.Features.FieldRecord.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using System.Text.RegularExpressions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Hrm.Application.Features.BankAccountType.Requests.Commands;
using Hrm.Application.Features.FieldRecord.Requests.Queries;
using Microsoft.AspNetCore.Hosting;
using Hrm.Application.Helpers;

namespace Hrm.Application.Features.FieldRecord.Handlers.Commands
{
    public class CreateFieldRecordCommandHandler: IRequestHandler<CreateFieldRecordCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FormHelper _formHelper;

        public CreateFieldRecordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _formHelper = new FormHelper(unitOfWork);
        }

        public async Task<BaseCommandResponse> Handle(CreateFieldRecordCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFieldRecordDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FieldRecordDto);


            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            // Check If the field is in schema
            var formRecord = await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Get(request.FieldRecordDto.FormRecordId);

            var formId = formRecord.FormId;

            if(!await _formHelper.CheckValidField(formId, request.FieldRecordDto.FieldId))
            {
                throw new BadRequestException("Invalid Field");
            }


            if(await _formHelper.CheckDataType(request.FieldRecordDto.FieldValue,request.FieldRecordDto.FieldId)==false)
            {
                throw new BadRequestException("Data type is invalid");
            }

            

            var fieldRecord = _mapper.Map<Hrm.Domain.FieldRecord>(request.FieldRecordDto);

            var formField = await _unitOfWork.Repository<Hrm.Domain.FormField>().Get(request.FieldRecordDto.FieldId);

            if(await _formHelper.CheckMultipleValue(request.FieldRecordDto.FormRecordId, request.FieldRecordDto.FieldId))
            {
                throw new BadRequestException("Multiple value for this field is not allowed");
            }

            // Check If It Has selectable option, then the values from selectable option are allowed

            if(!await _formHelper.IsValidSelectable(request.FieldRecordDto.FieldValue, request.FieldRecordDto.FieldId))
            {
                throw new BadRequestException("Invalid Option Selected");
            }

            await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Add(fieldRecord);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = fieldRecord.FieldRecordId;

            return response;
        }

        public async Task<bool> CheckDataType(string value, int FieldId)
        {
            var field = await _unitOfWork.Repository<Hrm.Domain.FormField>().Where(x => x.FieldId == FieldId)
               .Include(x => x.FieldType)
               .FirstOrDefaultAsync();

            if(field == null)
            {
                return true;
            }

            var ValidationRegex = field.FieldType.ValidationRegex;

            if(ValidationRegex == null)
            {
                return true;
            }

            string pattern = $@"{ValidationRegex}";
            Match match = Regex.Match(value, pattern);
            if (match == Match.Empty)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CheckValidField(int formId, int fieldId)
        {
            bool isValidField = await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Where(x => x.FieldId == fieldId && x.FormId == formId).AnyAsync();

            return isValidField;
        }
    }
}
