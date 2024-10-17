using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Form.Requests.Commands;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using Hrm.Application.DTOs.FieldRecord;
using Hrm.Application.DTOs.FormRecord;
using Hrm.Application.Helpers;
using Hrm.Application.DTOs.Form;
using Hrm.Application.Responses;

namespace Hrm.Application.Features.Form.Handlers.Commands
{
    public class CreateFormDataCommandHandler: IRequestHandler<CreateFormDataCommand, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FormHelper formHelper;

        public CreateFormDataCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            formHelper = new FormHelper(unitOfWork);
        }

        public async Task<object> Handle(CreateFormDataCommand request, CancellationToken cancellationToken)
        {

            var response = new BaseCommandResponse();
            if(request.formData.EmpId==null||request.formData.EmpId==0)
            {
                throw new BadRequestException("Employee Id is required");
            }

            FormRecordDto formRecordDto = new FormRecordDto();
            formRecordDto.RecordId = 0;
            formRecordDto.FormId = request.formData.FormId;
            formRecordDto.ReportFrom = request.formData.ReportFrom;
            formRecordDto.ReportTo = request.formData.ReportTo;
            formRecordDto.ReportingOfficerId = request.formData.ReportingOfficerId;
            formRecordDto.CounterSignatoryId = request.formData.CounterSignatoryId;
            formRecordDto.ReceiverId = request.formData.ReceiverId;
            
            formRecordDto.EmpId = (int)request.formData.EmpId;
            formRecordDto.IsActive = true;

            var employee = await _unitOfWork.Repository<Hrm.Domain.EmpBasicInfo>().Get(formRecordDto.EmpId);
            if(employee==null)
            {
                throw new NotFoundException(nameof(employee),formRecordDto.EmpId);
            }

            var formRecord = _mapper.Map<Hrm.Domain.FormRecord>(formRecordDto);

            await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Add(formRecord);
            await _unitOfWork.Save();

            foreach (var section in request.formData.Sections) { 
                foreach (var field in section.Fields)
                {
                    //if(field.FieldValue==""&&field.IsRequired) {
                    //    await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Delete(formRecord);
                    //    await _unitOfWork.Save();
                    //    throw new BadRequestException(field.FieldName + " is required");
                    //}

                    await CheckValidField(field,formRecord.RecordId);

                    var fieldRecordDto = new FieldRecordDto();
                    fieldRecordDto.FieldRecordId = 0;
                    fieldRecordDto.FieldValue = field.FieldValue;
                    fieldRecordDto.FieldId = field.FieldId;
                    fieldRecordDto.FormRecordId = formRecord.RecordId;
                    fieldRecordDto.IsActive = true;
                    fieldRecordDto.Remark = field.Remark;

                    var fieldRecord = _mapper.Map<Hrm.Domain.FieldRecord>(fieldRecordDto);
                    await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Add(fieldRecord);
                }
            }
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = formRecord.RecordId;

            return response;
        }

        public async Task CheckValidField(FormFieldValDto formField,int recordId)
        {
            if (formField.FieldValue == "")
                return;
            var formRecord = await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Get(recordId);

            if(!await formHelper.CheckDataType(formField.FieldValue,formField.FieldId))
            {
                await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Delete(formRecord);
                await _unitOfWork.Save();
                throw new BadRequestException(formField.FieldName + " datatype is invalid");
                
            }

            if(await formHelper.CheckMultipleValue(recordId,formField.FieldId))
            {
                await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Delete(formRecord);
                await _unitOfWork.Save();
                throw new BadRequestException("Multiple Value for " + formField.FieldName + " is not allowed");
            }

            if(!await formHelper.IsValidSelectable(formField.FieldValue,formField.FieldId))
            {
                await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Delete(formRecord);
                await _unitOfWork.Save();
                throw new BadRequestException("Invalid Option for this " + formField.FieldName);
            }
        }
    }
}
