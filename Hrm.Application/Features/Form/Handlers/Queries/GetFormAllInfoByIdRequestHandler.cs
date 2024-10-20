using AutoMapper;
using Hrm.Application.Contracts.Persistence;
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
using System.Net.Http.Headers;

namespace Hrm.Application.Features.Form.Handlers.Queries
{
    public class GetFormAllInfoByIdRequestHandler: IRequestHandler<GetFormAllInfoByIdRequest,object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFormAllInfoByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetFormAllInfoByIdRequest request, CancellationToken cancellationToken)
        {
            var form = await _unitOfWork.Repository<Hrm.Domain.Form>().Get(request.FormId);

            if(form==null)
            {
                throw new NotFoundException(nameof(form),request.FormId);

            }

            var formDto = _mapper.Map<FormDto>(form);

            //var sections = await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Where(x => x.FormId == formDto.FormId).Select(x => x.Section).Distinct().ToListAsync();

            var sections = await _unitOfWork.Repository<Hrm.Domain.FormSection>().Where(fs => fs.FormId == request.FormId).ToListAsync();


            List<FormSectionDto> sectionsWithField = new List<FormSectionDto>();

            foreach (var section in sections)
            {
                var fieldIds = await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Where(x => x.FormId == request.FormId && x.IsActive == true && x.SectionId == section.FormSectionId).Select(x => x.FieldId).ToListAsync();

                var fieldInfo = await _unitOfWork.Repository<Hrm.Domain.FormField>().Where(x => fieldIds.Contains(x.FieldId))
                    .Include(x=>x.FieldType)
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
                    HasMultipleValue=(bool)x.HasMultipleValue,
                    HasSelectable=(bool)x.HasSelectable,
                    FieldValue = "",
                    Remark = ""
                }).ToList();


                // Add Selectable Option If any
                foreach(var field in fieldInfoDto)
                {
                    List<SelectableOptionDto> options = new List<SelectableOptionDto>();
                    if(field.HasSelectable==true)
                    {
                        var selectableOption = await _unitOfWork.Repository<Hrm.Domain.SelectableOption>().Where(x => x.FieldId == field.FieldId && x.IsActive == true).ToListAsync();

                        var selectableOptionDto = _mapper.Map<List<SelectableOptionDto>>(selectableOption);
                        field.Options = selectableOptionDto;
                    }

                    if(field.HTMLTagName=="daterange")
                    {
                        List<int> tempFieldIds = await _unitOfWork.Repository<Domain.FormGroup>().Where(x=>x.ParentFieldId == field.FieldId&&x.IsActive==true).Select(x=>x.FormFieldId).ToListAsync();

                        var childField = await _unitOfWork.Repository<Hrm.Domain.FormGroup>().Where(x => x.ParentFieldId == field.FieldId && x.IsActive == true)
                            .Include(x => x.ChildField)
                            .ThenInclude(x=>x.FieldType).OrderBy(x => x.OrderNo).Select(x => x.ChildField).ToListAsync();



                        //var childField = await _unitOfWork.Repository<Domain.FormField>().Where(x => tempFieldIds.Contains(x.FieldId))
                           // .Include(x => x.FieldType).ToListAsync();
                        field.ChildFields = _mapper.Map<List<FormFieldDto>>(childField).Select(x => new FormFieldValDto
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
                            HasSelectable = (bool)x.HasSelectable,
                            FieldValue = "",
                            Remark = ""
                        }).ToList();
                    }
                }


                var signatureUrl="";
                if(section.EmpId!=null)
                {
                    var signature = _unitOfWork.Repository<Domain.EmpPhotoSign>().Where(x => x.EmpId == section.EmpId).FirstOrDefault();

                    if(signature!=null)
                    {
                        signatureUrl = signature.SignatureUrl;
                    }
                }
                sectionsWithField.Add(new FormSectionDto
                {
                    SectionId = (int) section.FormSectionId,
                    SectionName = section.FormSectionName,
                    EmpId = section.EmpId,
                    SignaturePhotoUrl = signatureUrl,
                    Fields = fieldInfoDto
                });
            }
            

            var formDetail = new FormDataDto
            {
                FormId = request.FormId,
                FormName = form.FormName,
                Description = form.Description,
                EmpId = 0,
                Sections =  sectionsWithField
            };

            return formDetail;

        }
    }
}
