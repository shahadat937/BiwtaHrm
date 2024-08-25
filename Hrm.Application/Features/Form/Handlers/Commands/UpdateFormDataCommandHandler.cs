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



            foreach (var section in request.formData.Sections)
            {
                foreach(var field in section.Fields)
                {
                    if(!await _formHelper.CheckValidField(request.formData.FormId,field.FieldId))
                    {
                        throw new BadRequestException("Invalid Field: "+field.FieldName);
                    }

                    if(!await _formHelper.CheckDataType(field.FieldValue,field.FieldId))
                    {
                        throw new BadRequestException("Field Value (" + field.FieldName + ") is invalid");
                    }

                    if(!await _formHelper.IsValidSelectable(field.FieldValue,field.FieldId))
                    {
                        throw new BadRequestException("Field Option (" + field.FieldName + ") is invalid");
                    }

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
    }
}
