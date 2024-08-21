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

            var sections = await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Where(x => x.FormId == formDto.FormId).Select(x => x.Section).Distinct().ToListAsync();

            List<object> sectionsWithField = new List<object>();

            foreach (var section in sections)
            {
                var fieldIds = await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Where(x => x.FormId == request.FormId && x.IsActive == true && x.Section == section).Select(x => x.FieldId).ToListAsync();

                var fieldInfo = await _unitOfWork.Repository<Hrm.Domain.FormField>().Where(x => fieldIds.Contains(x.FieldId))
                    .Include(x=>x.FieldType)
                    .ToListAsync();

                var fieldInfoDto = _mapper.Map<List<FormFieldDto>>(fieldInfo).Select(x => new
                {
                    x.FieldId,
                    x.FieldName,
                    x.Description,
                    x.IsRequired,
                    x.FieldTypeId,
                    x.FieldTypeName,
                    x.HTMLTagName,
                    x.HTMLInputType,
                    x.HasMultipleValue,
                    x.HasSelectable,
                    FieldValue = "",
                }).ToList();

                sectionsWithField.Add(new
                {
                    SectionId = section,
                    Fields = fieldInfoDto
                });
            }
            

            var formDetail = new
            {
                FormId = request.FormId,
                FormName = form.FormName,
                Description = form.Description,
                Sections = sectionsWithField
            };

            return formDetail;

        }
    }
}
