using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Union;
using Hrm.Application.Features.Form.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.Form;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.FormField;
using Hrm.Application.DTOs.SelectableOption;

namespace Hrm.Application.Features.Form.Handlers.Queries
{
    public class GetAllFormDataRequestHandler: IRequestHandler<GetAllFormDataRequest,object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllFormDataRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAllFormDataRequest request, CancellationToken cancellationToken)
        {
            var formRecord = await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Get(request.FormRecordId);

            if(formRecord == null)
            {
                throw new NotFoundException(nameof(formRecord), request.FormRecordId);
            }

            var form = await _unitOfWork.Repository<Hrm.Domain.Form>().Get(formRecord.FormId);

            if(form==null)
            {
                throw new BadRequestException("Form is invalid");
            }

            var formDataDto = new FormDataDto
            {
                FormId = formRecord.FormId,
                RecordId = formRecord.RecordId,
                FormName = form.FormName,
                Description = form.Description,
                ReportFrom = formRecord.ReportFrom,
                ReportTo = formRecord.ReportTo,
                EmpId = formRecord.EmpId
            };

            var sections = await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Where(x => x.FormId == formRecord.FormId).Select(x => x.Section).Distinct().ToListAsync();

            List<FormSectionDto> sectionDtos = new List<FormSectionDto>();

            foreach(var section in sections)
            {
                var fieldIds = await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Where(x => x.FormId == formRecord.FormId && x.IsActive == true && x.Section == section).Select(x => x.FieldId).ToListAsync();

                var fieldInfo = await _unitOfWork.Repository<Hrm.Domain.FormField>().Where(x => fieldIds.Contains(x.FieldId))
                    .Include(x => x.FieldType)
                    .ToListAsync();

                var fieldInfoDto = _mapper.Map<List<FormFieldDto>>(fieldInfo).Select(x => new FormFieldValDto
                {
                    FieldId = x.FieldId,
                    FieldName = x.FieldName,
                    Description = x.Description,
                    IsRequired = (bool)x.IsRequired,
                    FieldTypeId = x.FieldTypeId,
                    FieldTypeName = x.FieldTypeName,
                    HTMLTagName = x.HTMLTagName,
                    HTMLInputType = x.HTMLInputType,
                    HasMultipleValue = (bool)x.HasMultipleValue,
                    HasSelectable = (bool)x.HasSelectable
                }).ToList();

                foreach(var field in fieldInfoDto)
                {
                    var fieldRecord = await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Where(x => x.FieldId == field.FieldId && x.FormRecordId == formRecord.RecordId).FirstOrDefaultAsync();

                    if(fieldRecord == null)
                    {
                        continue;
                    }

                    field.Remark = fieldRecord.Remark;
                    field.FieldRecordId = fieldRecord.FieldRecordId;
                    field.FieldValue = fieldRecord.FieldValue;

                    if(field.HasSelectable==true)
                    {
                        var options = await _unitOfWork.Repository<Hrm.Domain.SelectableOption>().Where(x => x.FieldId == field.FieldId && x.IsActive == true).ToListAsync();

                        var optionDtos = _mapper.Map<List<SelectableOptionDto>>(options);

                        field.Options = optionDtos;
                    }
                }

                var sectionDto = new FormSectionDto
                {
                    SectionId = (int)section,
                    Fields = fieldInfoDto

                };

                sectionDtos.Add(sectionDto);
            }


            formDataDto.Sections = sectionDtos;

            return formDataDto;
            
        }
    }
}
