using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Form.Requests.Commands;
using Hrm.Application.Helpers;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.FieldRecord;
using Hrm.Application.Enum;
using Hrm.Application.DTOs.Form;

namespace Hrm.Application.Features.Form.Handlers.Commands
{
    public class UpdateFormDataCommandHandler: IRequestHandler<UpdateFormDataCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FormHelper _formHelper;

        public UpdateFormDataCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _formHelper = new FormHelper(unitOfWork);
        }

        public async Task<BaseCommandResponse> Handle(UpdateFormDataCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            if(request.formData.RecordId==null)
            {
                throw new BadRequestException("Form Record Id is required");
            }

            var formRecord = await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Get((int)request.formData.RecordId);


            if(formRecord==null)
            {
                throw new NotFoundException(nameof(formRecord), request.formData.RecordId);
            }

            

            // Validate if the form can be modified according to the current form status and the user
            if(request.UpdateRole == (int) AppraisalRole.User)
            {

            } else if(request.UpdateRole == (int) AppraisalRole.ReportingOfficer) // 
            {
                if(formRecord.CounterSignatoryApproval)
                {
                    throw new BadRequestException("Counter Signatory has already signed the apprisal");
                }

                formRecord.ReportingOfficerApproval = true;

            } else if(request.UpdateRole == (int) AppraisalRole.CounterSignatory)
            {
                if(formRecord.ReceiverApproval)
                {
                    throw new BadRequestException("Receiver has already received the apprisal");
                }

                formRecord.CounterSignatoryApproval = true;

            } else if(request.UpdateRole == (int) AppraisalRole.Receiver)
            {
                formRecord.ReceiverApproval = true;
            }

            if(request.UpdateRole != (int) AppraisalRole.User)
            {
                await _unitOfWork.Repository<Domain.FormRecord>().Update(formRecord);
            }

            foreach (var section in request.formData.Sections)
            {
                foreach(var field in section.Fields)
                {
                    await CheckValidField(field, formRecord.RecordId);

                    var fieldDto = new FieldRecordDto();
                    fieldDto.FieldId = field.FieldId;
                    fieldDto.FieldName = field.FieldName;
                    fieldDto.FieldRecordId = field.FieldRecordId;
                    fieldDto.FieldValue = field.FieldValue;
                    fieldDto.FormRecordId = formRecord.RecordId;
                    fieldDto.Remark = field.Remark;

                    var fieldRecord = await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Get(fieldDto.FieldRecordId);

                    if(fieldRecord==null)
                    {
                        throw new NotFoundException(nameof(fieldRecord), fieldDto.FieldRecordId);
                    }

                    if(field.HTMLTagName == "daterange"||field.HTMLTagName=="group")
                    {
                        foreach(var childField in field.ChildFields)
                        {
                            await CheckValidField(childField,formRecord.RecordId);

                            var childFieldRecord = await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Get(childField.FieldRecordId);

                            if(childFieldRecord==null)
                            {
                                throw new NotFoundException(nameof(fieldRecord), fieldDto.FormRecordId);
                            }

                            var childFieldDto = new FieldRecordDto();
                            childFieldDto.FieldId = childField.FieldId;
                            childFieldDto.FieldName = childField.FieldName;
                            childFieldDto.FieldRecordId = childField.FieldRecordId;
                            childFieldDto.FieldValue = childField.FieldValue;
                            childFieldDto.FormRecordId = formRecord.RecordId;
                            childFieldDto.Remark = childField.Remark;

                            _mapper.Map(childFieldDto, childFieldRecord);
                            await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Update(childFieldRecord);
                        }
                    }



                    _mapper.Map(fieldDto, fieldRecord);
                    await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Update(fieldRecord);
                }
            }

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = (int)formRecord.RecordId;


            return response;
        }

        public async Task CheckValidField(FormFieldValDto field, int recordId)
        {
            if (field.FieldValue == "")
                return;

            //if (field.FieldValue != "" && !await _formHelper.CheckValidField(request.formData.FormId, field.FieldId))
            //{
              //  throw new BadRequestException("Invalid Field: " + field.FieldName);
            //}

            if (field.FieldValue != "" && !await _formHelper.CheckDataType(field.FieldValue, field.FieldId))
            {
                throw new BadRequestException("Field Value (" + field.FieldName + ") is invalid");
            }

            if (field.FieldValue != "" && !await _formHelper.IsValidSelectable(field.FieldValue, field.FieldId))
            {
                throw new BadRequestException("Field Option (" + field.FieldName + ") is invalid");
            }
        }
    }
}
